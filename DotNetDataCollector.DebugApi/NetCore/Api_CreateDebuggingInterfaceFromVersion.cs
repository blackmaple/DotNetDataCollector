namespace DotNetDataCollector.DebugApi
{
    public readonly unsafe struct Api_CreateDebuggingInterfaceFromVersion(nint ptr)
    {
        public const string Api = "CreateDebuggingInterfaceFromVersion";
        /*
         HRESULT CreateDebuggingInterfaceFromVersion (
    [in]  LPCWSTR      szDebuggeeVersion,
    [out] IUnknown**   ppCordb,
);
         */
        readonly delegate* unmanaged[Cdecl]<nint, out nint, int> _func = (delegate* unmanaged[Cdecl]<nint, out nint, int>)ptr;


        public static implicit operator Api_CreateDebuggingInterfaceFromVersion(nint ptr) => new(ptr);
        public static implicit operator nint(Api_CreateDebuggingInterfaceFromVersion fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }
    }
}
