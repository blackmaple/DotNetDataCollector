// See https://aka.ms/new-console-template for more information


using DotNetDataCollector.DebugApi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;


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
//if (debugapi.TryGetCLRs(out var dotNetClrInfos))
//{
//    foreach(var assembly in dotNetClrInfos)
//    {
//        Console.WriteLine(assembly.Dll);
//    }
//}
debugapi.Test();

Console.ReadKey();