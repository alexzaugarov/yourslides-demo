using Ghostscript.NET;

namespace Yourslides.FileHandler.GhostscriptHelpers {
    public class GhostscriptPngDeviceFactory : IGhostscriptImageDeviceFactory{

        public IGhostscriptImageDeviceWrap CreateDevice(int dpi) {
            var device = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png16m) {
                Resolution = dpi,
                GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4,
                TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4
            };
            return new GhostscriptImageDeviceWrapBase(device, ".png");
        }
    }
}