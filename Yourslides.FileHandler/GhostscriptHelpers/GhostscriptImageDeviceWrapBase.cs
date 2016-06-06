using System.IO;
using Ghostscript.NET;

namespace Yourslides.FileHandler.GhostscriptHelpers {
    public class GhostscriptImageDeviceWrapBase : IGhostscriptImageDeviceWrap{
        public GhostscriptImageDeviceWrapBase(GhostscriptImageDevice device, string extension) {
            Device = device;
            Extension = extension;
        }

        public GhostscriptImageDevice Device { get; protected set; }
        public string Extension { get; protected set; }

        public string OutputDir {
            set {
                Device.OutputPath = Path.Combine(value, "%d" + Extension);
            }
        }

        public string InputFile {
            set {
                Device.InputFiles.Clear();
                Device.InputFiles.Add(value);
            }
        }
    }
}