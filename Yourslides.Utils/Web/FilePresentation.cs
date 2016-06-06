using System.IO;

namespace Yourslides.Utils.Web {
    public class FilePresentation {
        public string Filename { get; set; }
        public Stream InputStream { get; set; }
        public string ContentType { get; set; }

        public void SaveAs(string destination, bool overwrite = false) {
            using (var file = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew)) {
                InputStream.CopyTo(file);
            }
            InputStream.Close();
        }
    }
}