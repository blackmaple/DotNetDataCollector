using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{

    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnmanagedLPWStr(nint ptr)
    {
        [MarshalAs(UnmanagedType.SysInt)]
        private readonly nint _ptr = ptr;


        public ReadOnlySpan<char> AsReadOnlySpan()
        {
            return MemoryMarshal.CreateReadOnlySpanFromNullTerminated((char*)_ptr);
        }

        public override string? ToString()
        {
            if (_ptr == nint.Zero)
            {
                return default;
            }
            return new string((char*)_ptr);
        }
    }
}
