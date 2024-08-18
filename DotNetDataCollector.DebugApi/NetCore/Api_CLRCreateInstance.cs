namespace DotNetDataCollector.DebugApi
{
    public readonly unsafe struct Api_CLRCreateInstance(nint ptr)
    {
        public const string Api = "CLRCreateInstance";
        /*
         HRESULT CLRCreateInstance (
    [in]  REFCLSID  clsid,
    [in]  REFIID     riid,
    [out] LPVOID  * ppInterface
);
         */
        readonly delegate* unmanaged[Cdecl]<nint, nint, out nint,int> _func = (delegate* unmanaged[Cdecl]<nint, nint, out nint, int>)ptr;
        public static implicit operator Api_CLRCreateInstance(nint ptr) => new(ptr);
        public static implicit operator nint(Api_CLRCreateInstance fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }
    }
}
