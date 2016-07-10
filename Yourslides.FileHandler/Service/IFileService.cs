using System.Collections.Generic;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Utils.Web;

namespace Yourslides.FileHandler.Service {
    public interface IFileService {
        void SaveFile(FilePresentation file, Presentation presentation);
        string GetSlide(long presentationId, int slideIndex, string quality);
        void DeleteDirectory(string id);
    }
}