using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnmanagedHRESULT(int code)
    {
        public const int S_OK = 0;
        public const int S_FALSE = 1;
        public const int E_FAIL = unchecked((int)0x80004005);
        public const int E_INVALIDARG = unchecked((int)0x80070057);
        public const int E_NOTIMPL = unchecked((int)0x80004001);
        public const int E_NOINTERFACE = unchecked((int)0x80004002);

        public const int HRESULT_ERROR_PARTIAL_COPY = unchecked((int)0x8007012b);
        public const int HRESULT_ERROR_BAD_LENGTH = unchecked((int)0x80070018);

        [MarshalAs(UnmanagedType.Error)]
        private readonly int _code = code;

        public static implicit operator UnmanagedHRESULT(int code) => new(code);
        public static implicit operator int(UnmanagedHRESULT hr) => hr._code;

        public bool OK()
        {
            return _code == S_OK;
        }

        public bool ThrowIfError()
        {
            var ex = Marshal.GetExceptionForHR(_code);
            if (ex is null)
            {
                return true;
            }
            return DotNetDataCollectorException.Throw<bool>(ex.Message);
        }
    }
}
