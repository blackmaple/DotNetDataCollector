// See https://aka.ms/new-console-template for more information


using DotNetDataCollector.DebugApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


var serviceDescriptors = new ServiceCollection();
serviceDescriptors.AddLogging(p => 
{
    p.ClearProviders();
    p.AddDebug();
    p.AddConsole();
});
serviceDescriptors.AddSingleton<DebugApiLoader>();
var service = serviceDescriptors.BuildServiceProvider();
var debugapi = service.GetRequiredService<DebugApiLoader>();
debugapi.TryLoadDotNetDebugApi();
debugapi.Get();
Console.ReadKey();