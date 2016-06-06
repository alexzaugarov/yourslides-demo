using Ghostscript.NET;

namespace Yourslides.FileHandler.GhostscriptHelpers {
    public interface IGhostscriptImageDeviceFactory {
        IGhostscriptImageDeviceWrap CreateDevice(int dpi);
    }
}