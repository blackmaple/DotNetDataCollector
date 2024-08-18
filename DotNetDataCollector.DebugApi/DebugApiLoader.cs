using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    public partial class DebugApiLoader(ILogger<DebugApiLoader> logger)
    {
        public ILogger Logger { get; } = logger;
        public EnumDotNetRuntimeType RuntimeType { get; private set; } = EnumDotNetRuntimeType.UNKNOWN;

        #region Api
        //public static Guid CLSID_CLRDebugging { get; } = new Guid("BACC578D-FBDD-48A4-969F-02D932B74634");
        //public static Guid CLSID_CLRDebuggingLegacy { get; } = new Guid("DF8395B5-A4BA-450b-A77C-A9A47762C520");
        public static Guid IID_ICLRDebugging { get; } = new("D28F3C5A-9634-4206-A509-477552EEFB10");
        public static Guid CLSID_ICLRDebugging { get; } = new("BACC578D-FBDD-48A4-969F-02D932B74634");
        internal Api_CloseCLREnumeration CloseCLREnumeration;
        internal Api_CLRCreateInstance CLRCreateInstance;
        internal Api_CreateDebuggingInterfaceFromVersion CreateDebuggingInterfaceFromVersion;
        internal Api_CreateVersionStringFromModule CreateVersionStringFromModule;
        internal Api_EnumerateCLRs EnumerateCLRs;
        #endregion

        #region netCore

        const string DebugDllName = "dbgshim.dll";
        const string RuntimeDll = "System.Runtime.dll";
        const string DotNetSDK_x64 = "C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\";
        const string DotNetSDK_x86 = "C:\\Program Files (x86)\\dotnet\\shared\\Microsoft.NETCore.App\\";
        public bool LoadRuntime_DotNetCore_Imp(nint hModule)
        {
            var init = true;
            init &= TryCreate(hModule, Api_CloseCLREnumeration.Api, out CloseCLREnumeration);
            init &= TryCreate(hModule, Api_CLRCreateInstance.Api, out CLRCreateInstance);
            init &= TryCreate(hModule, Api_CreateDebuggingInterfaceFromVersion.Api, out CreateDebuggingInterfaceFromVersion);
            init &= TryCreate(hModule, Api_CreateVersionStringFromModule.Api, out CreateVersionStringFromModule);
            init &= TryCreate(hModule, Api_EnumerateCLRs.Api, out EnumerateCLRs);
            return init;
        }
        static IEnumerable<string> GetSearchDebugDll_ProcessModule()
        {

            using var process = Process.GetCurrentProcess();
            foreach (ProcessModule m in process.Modules)
            {
                if (m.ModuleName.Equals(RuntimeDll, StringComparison.OrdinalIgnoreCase))
                {
                    var info = System.IO.Directory.GetParent(m.FileName);
                    if (info is not null)
                    {
                        var dll = Path.Combine(info.FullName, DebugDllName);
                        if (System.IO.File.Exists(dll))
                        {
                            yield return dll;
                        }
                    }
                }
            }




        }
        static IEnumerable<string> GetSearchDebugDll_SDK()
        {
            var sdkPath = Environment.Is64BitProcess ? DotNetSDK_x64 : DotNetSDK_x86;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var fileInfo = System.IO.Directory.GetFiles(sdkPath, DebugDllName, SearchOption.AllDirectories);
                Array.Sort(fileInfo, Comparer<string>.Create(static (x, y) =>
                {
                    return y.CompareTo(x);
                }));
                foreach (var dll in fileInfo)
                {
                    if (System.IO.File.Exists(dll))
                    {
                        yield return dll;
                    }

                }
            }
        }
        static IEnumerable<string> GetSearchDebugDll_BaseDirectory()
        {
            var sdkPath = AppContext.BaseDirectory;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var fileInfo = System.IO.Directory.GetFiles(sdkPath, DebugDllName, SearchOption.AllDirectories);

                foreach (var dll in fileInfo)
                {
                    if (System.IO.File.Exists(dll))
                    {
                        yield return dll;
                    }

                }
            }
        }
        static bool TryGetDebugDllHandle_DotNetCore([MaybeNullWhen(false)] out nint hModule)
        {
            foreach (var dll in GetSearchDebugDll_ProcessModule())
            {
                if (NativeLibrary.TryLoad(dll, out hModule))
                {
                    return true;
                }
            }
            foreach (var dll in GetSearchDebugDll_BaseDirectory())
            {
                if (NativeLibrary.TryLoad(dll, out hModule))
                {
                    return true;
                }
            }
            foreach (var dll in GetSearchDebugDll_SDK())
            {
                if (NativeLibrary.TryLoad(dll, out hModule))
                {
                    return true;
                }
            }
            return NativeLibrary.TryLoad(DebugDllName, out hModule);



        }
        #endregion

        #region netFx

        #endregion
        EnumDotNetRuntimeType GetDotNetDebugModuleHandle(out nint moduleHandle)
        {
            var runtimeType = EnumDotNetRuntimeType.ERROR;
            if (TryGetDebugDllHandle_DotNetCore(out moduleHandle))
            {
                return EnumDotNetRuntimeType.DOTNETCORE;
            }
            else
            {
                DotNetDataCollectorException.Throw<EnumDotNetRuntimeType>("not imp");
            }
            return runtimeType;
        }
        public bool TryLoadDotNetDebugApi()
        {
            if (this.RuntimeType == EnumDotNetRuntimeType.UNKNOWN)
            {
                this.RuntimeType = GetDotNetDebugModuleHandle(out var moduleHandle);
                return this.RuntimeType switch
                {
                    EnumDotNetRuntimeType.DOTNETCORE => LoadRuntime_DotNetCore_Imp(moduleHandle),
                    _ => false
                };
            }

            return this.RuntimeType != EnumDotNetRuntimeType.ERROR;
        }

        private bool TryCreate<T_PTR_FUNC>(nint hModule, string name, out T_PTR_FUNC ptr_func)
           where T_PTR_FUNC : unmanaged
        {
            NativeLibrary.TryGetExport(hModule, name, out var address);
            this.Logger.LogInformation("{funcName}=>0x{address}", name, address.ToString("X8"));
            ptr_func = Unsafe.As<nint, T_PTR_FUNC>(ref address);
            return address != nint.Zero;
        }


        #region Test
        public unsafe void Get()
        {
            EnumerateCLRs.Invoke(Environment.ProcessId, out var ppHandleArrayOut, out var ppStringArrayOut, out var pdwArrayLengthOut);
            var handles = new ReadOnlySpan<nint>(ppHandleArrayOut, pdwArrayLengthOut);
            foreach (var h in handles)
            {
                this.Logger.LogInformation("handle:{h}", h.ToString("X8"));
            }
            var lpStrs = new ReadOnlySpan<nint>(ppStringArrayOut, pdwArrayLengthOut);

            foreach (var lpStr in lpStrs)
            {
                this.Logger.LogInformation("str:{s}", new string((char*)lpStr));
            }

            CloseCLREnumeration.Invoke(ppHandleArrayOut, ppStringArrayOut, pdwArrayLengthOut);
        }
        #endregion
    }



    public class Test_DebugApiLoader(ILogger<Test_DebugApiLoader> logger) : DebugApiLoader(logger)
    {
        public bool Test_TryLoadDotNetDebugApi() => TryLoadDotNetDebugApi();
    }
}
