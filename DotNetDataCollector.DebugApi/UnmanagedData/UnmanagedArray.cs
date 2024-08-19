using System.Runtime.CompilerServices;

namespace DotNetDataCollector.DebugApi
{
    public readonly unsafe struct UnmanagedArray<T_PointerType>(nint ptr, int count)
    where T_PointerType : unmanaged
    {

        private readonly nint _ptr = ptr;
        private readonly int _count = count;

        public ReadOnlySpan<T_PointerType> AsReadOnlySpan()
        {
            return new ReadOnlySpan<T_PointerType>(_ptr.ToPointer(), _count);
        }

        public T_PointerType[] ToArray()
        {
            return AsReadOnlySpan().ToArray();
        }

        public ref T_PointerType RefElementAt(int index)
        {
            return ref RefElementAt(_ptr, index);
        }

        public T_PointerType this[int i] => RefElementAt(i);


        public static ref T_PointerType RefElementAt(nint array, int index)
        {
            ref var ref_Type = ref Unsafe.AsRef<T_PointerType>(array.ToPointer());
            return ref Unsafe.Add(ref ref_Type, index);
        }

    }

}
