namespace DotNetDataCollector.DebugApi
{
    public readonly struct DotNetClrInfo(nint handle, string? dll)
    {
        public nint Handle { get; } = handle;
        public string? Dll { get; } = dll;

    }
}
