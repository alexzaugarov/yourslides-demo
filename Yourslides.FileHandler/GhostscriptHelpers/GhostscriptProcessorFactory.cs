using Ghostscript.NET;
using Ghostscript.NET.Processor;

namespace Yourslides.FileHandler.GhostscriptHelpers {
    public class GhostscriptProcessorFactory {
        public GhostscriptProcessor CreateGhostscriptProcessor() {
            return new GhostscriptProcessor(GhostscriptVersionInfo.GetLastInstalledVersion());
        } 
    }
}