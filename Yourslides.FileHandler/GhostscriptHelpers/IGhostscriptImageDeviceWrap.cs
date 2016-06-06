using Ghostscript.NET;

namespace Yourslides.FileHandler.GhostscriptHelpers {
    public interface IGhostscriptImageDeviceWrap {
        GhostscriptImageDevice Device { get; }
        string Extension { get; }
        string OutputDir { set; }
        string InputFile { set; }
    }
}