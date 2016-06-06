using Yourslides.Model;
using Yourslides.Utils.Web;

namespace Yourslides.FileHandler.Service {
    public interface IFileService {
        void SaveFile(FilePresentation file, Presentation presentation);
        string GetSlide(long presentationId, int slideIndex, string quality);
    }
}