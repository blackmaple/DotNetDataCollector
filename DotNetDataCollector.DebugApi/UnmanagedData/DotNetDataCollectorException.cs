using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDataCollector.DebugApi
{
    public sealed class DotNetDataCollectorException(int code, string msg) : Exception(msg)
    {
        public int Code { get; } = code;

        public DotNetDataCollectorException(string msg) : this(0, msg) { }

        [DoesNotReturn]
        public static void Throw(string msg)
        { 
            throw new DotNetDataCollectorException(msg);
        }

        [DoesNotReturn]
        public static T Throw<T>(string msg)
        {
            throw new DotNetDataCollectorException(msg);
        }
    }
}
