using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace DotNetDataCollector.DebugApi
{

    public readonly unsafe ref struct Ref_UnmanagedArray<T_PointerType>(nint ptr, int count)
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
            ref var ref_Type = ref Unsafe.AsRef<T_PointerType>(_ptr.ToPointer());
            return ref Unsafe.Add(ref ref_Type, index);
        }

        public T_PointerType this[int i] => RefElementAt(i);




    }

}
