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

        readonly delegate* unmanaged[Cdecl]<uint, char*, char*, uint, uint*, int> _func = (delegate* unmanaged[Cdecl]<uint, char*, char*, uint, uint*, int>)ptr;

        public int Invoke(uint pidDebuggee, char* szModuleName, char* pBuffer, uint cchBuffer, uint* pdwLength) => _func(pidDebuggee, szModuleName, pBuffer, cchBuffer, pdwLength);

    }

}
