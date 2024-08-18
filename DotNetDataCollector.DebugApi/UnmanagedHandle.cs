using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct UnmanagedHandle(nint ptr)
    {
        [MarshalAs(UnmanagedType.SysInt)]
        private readonly nint _ptr = ptr;


        public override string ToString()
        {
            return _ptr.ToString("X8");
        }

    }



}
