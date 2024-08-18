using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;

namespace DotNetDataCollector.DebugApi
{
    [GeneratedComInterface]
    [Guid("3faca0d2-e7f1-4e9c-82a6-404fd6e0aab8")]
    internal partial interface IFoo
    {
        [PreserveSig]
        int Method1(int i, out int j);

       // [PreserveSig]
        int Method2(float i);
    }

}
