using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnmanagedArray<T_PointerType>(nint ptr)
        where T_PointerType : unmanaged
    {
        [MarshalAs(UnmanagedType.SysInt)]
        private readonly nint _ptr = ptr;

        public nint UnmanagedPointer => _ptr;


        public ReadOnlySpan<T_PointerType> AsReadOnlySpan(int count)
        {
            return new ReadOnlySpan<T_PointerType>(_ptr.ToPointer(), count);
        }


    }



}
