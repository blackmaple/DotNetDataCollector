using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
        readonly delegate* unmanaged[Stdcall]<Guid*, Guid*, out nint, int> _func = (delegate* unmanaged[Stdcall]<Guid*, Guid*, out nint, int>)ptr;
        public static implicit operator Api_CLRCreateInstance(nint ptr) => new(ptr);
        public static implicit operator nint(Api_CLRCreateInstance fun) => (nint)fun._func;
        public override string ToString()
        {
            return ((nint)_func).ToString("X8");
        }


        public int Invoke(in Guid clsid, in Guid riid, out nint ppInterface)
        {
            fixed (global::System.Guid* fixed_clsid = &clsid)
            fixed (global::System.Guid* fixed_riid = &riid)
            {
                return _func(fixed_clsid, fixed_riid, out ppInterface);
            }

        }




    }

   
}
