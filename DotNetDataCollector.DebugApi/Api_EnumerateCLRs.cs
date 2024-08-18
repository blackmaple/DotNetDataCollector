using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
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
        readonly delegate* unmanaged[Cdecl]<uint, nint*, nint*, uint*, int> _func = (delegate* unmanaged[Cdecl]<uint, nint*, nint*, uint*, int>)ptr;

        public int Invoke([In] uint debuggeePID, [Out] nint* ppHandleArrayOut, [Out] nint* ppStringArrayOut, [Out] uint* pdwArrayLengthOut) => _func(debuggeePID, ppHandleArrayOut, ppStringArrayOut, pdwArrayLengthOut);
    }

}
