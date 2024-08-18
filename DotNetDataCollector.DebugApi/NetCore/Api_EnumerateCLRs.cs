using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Api_EnumerateCLRs(nint ptr)
    {
        public const string Api = "EnumerateCLRs";

        /*
         HRESULT EnumerateCLRs (
    [in]  DWORD      debuggeePID,
    [out] HANDLE**   ppHandleArrayOut,
    [out] LPWSTR**   ppStringArrayOut,
    [out] DWORD*     pdwArrayLengthOut
);
         */
        [MarshalAs(UnmanagedType.FunctionPtr)]
        readonly delegate* unmanaged[Stdcall]<
            int,
            out nint,
            out nint,
            out int,
            int> _func = (delegate* unmanaged[Stdcall]<
            int,
            out nint,
            out nint,
            out int,
            int>)ptr;

        public int Invoke(int debuggeePID,
            out nint ppHandleArrayOut,
            out nint ppStringArrayOut,
            out int pdwArrayLengthOut)
        {
            return _func(debuggeePID, out ppHandleArrayOut, out ppStringArrayOut, out pdwArrayLengthOut);
        }


        public static implicit operator Api_EnumerateCLRs(nint ptr) => new(ptr);
        public static implicit operator nint(Api_EnumerateCLRs fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }
    }






}
