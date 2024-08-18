namespace DotNetDataCollector.DebugApi
{
    public readonly unsafe struct Api_CreateVersionStringFromModule(nint ptr)
    {
        public const string Api = "CreateVersionStringFromModule";
        /*
         HRESULT CreateVersionStringFromModule (
    [in]  DWORD      pidDebuggee,
    [in]  LPCWSTR    szModuleName,
    [out, size_is(cchBuffer),
    length_is(*pdwLength)] LPWSTR Buffer,
    [in]  DWORD      cchBuffer,
    [out] DWORD*     pdwLength
);
         */

        readonly delegate* unmanaged[Cdecl]<uint, nint, nint, uint, out uint, int> _func = (delegate* unmanaged[Cdecl]<uint, nint, nint, uint, out uint, int>)ptr;

        //public UnmanagedHRESULT Invoke(uint pidDebuggee, char* szModuleName, char* pBuffer, uint cchBuffer, out uint pdwLength) => _func(pidDebuggee, szModuleName, pBuffer, cchBuffer, out pdwLength);


        public static implicit operator Api_CreateVersionStringFromModule(nint ptr) => new(ptr);
        public static implicit operator nint(Api_CreateVersionStringFromModule fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }
    }
}
