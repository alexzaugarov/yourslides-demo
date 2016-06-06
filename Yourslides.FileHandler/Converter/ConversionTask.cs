using Yourslides.Model;
using Yourslides.Utils.Web;

namespace Yourslides.FileHandler.Converter {
    public class ConversionTask {
        public Presentation Presentation { get; set; }
        public string InputFile { get; set; }
        public string OutputDir { get; set; }
    }
}