using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace DotNetDataCollector.DebugApi
{
    [GeneratedComInterface]
    [Guid("D28F3C5A-9634-4206-A509-477552EEFB10")]
    public partial interface ICLRDebugging
    {
        [PreserveSig]
        public int OpenVirtualProcess(
        [MarshalAs(UnmanagedType.SysInt)] nint moduleBaseAddress,
        [MarshalAs(UnmanagedType.SysInt)] nint dataTarget,
        [MarshalAs(UnmanagedType.SysInt)] nint libraryProvider,
        in ClrDebuggingVersion maxDebuggerSupportedVersion,
        in Guid riidProcess,
        out IntPtr process,
        out ClrDebuggingVersion version,
        out ClrDebuggingProcessFlags flags);

    }
}
