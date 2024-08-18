using DotNetDataCollector.DebugApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace DotNetDataCollector.Test
{
    [TestClass]
    public class DebugApiTest
    {

        [NotNull]
        public static IServiceProvider? ServiceProvider { get; set; }

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            var services = new ServiceCollection();
            services.AddLogging(p =>
            {
                p.ClearProviders();
                p.AddDebug();
            });
            services.AddSingleton<Test_DebugApiLoader>();
            ServiceProvider = services.BuildServiceProvider();
        }


        [TestMethod]
        public void Test_DebugApiLoader()
        {
            var debuger = ServiceProvider.GetService<Test_DebugApiLoader>();
            Debug.Assert(debuger is not null);
            Debug.Assert(debuger.Test_TryLoadDotNetDebugApi());
            debuger.Get();
        }
    }
}
