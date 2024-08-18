namespace DotNetDataCollector.DebugApi
{
    public readonly unsafe struct Api_CloseCLREnumeration(nint ptr)
    {
        public const string Api = "CloseCLREnumeration";
        /*
         HRESULT CloseCLREnumeration (
    [in]  DWORD      pHandleArray,
    [in]  LPWSTR**   pStringArray,
    [in]  DWORD*     dwArrayLength
);
         */
        readonly delegate* unmanaged[Stdcall]<nint, nint, int, int> _func = (delegate* unmanaged[Stdcall]<nint, nint, int, int>)ptr;

        public static implicit operator Api_CloseCLREnumeration(nint ptr) => new(ptr);
        public static implicit operator nint(Api_CloseCLREnumeration fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }


        public int Invoke(nint pHandleArray, nint pStringArray, int dwArrayLength) => _func(pHandleArray, pStringArray, dwArrayLength);
    }
}
