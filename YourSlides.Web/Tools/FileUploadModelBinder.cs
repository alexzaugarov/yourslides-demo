using System.Web.Mvc;
using Yourslides.Utils.Web;

namespace YourSlides.Web.Tools {
    public class FileUploadModelBinder : IModelBinder {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext) {
            var request = controllerContext.RequestContext.HttpContext.Request;
            var formUpload = request.Files.Count > 0;

            // find filename
            var xFileName = request.Headers["X-File-Name"];
            var qqFile = request["qqfile"];
            var formFilename = formUpload ? request.Files[0].FileName : null;

            var upload = new FilePresentation {
                Filename = xFileName ?? qqFile ?? formFilename,
                InputStream = formUpload ? request.Files[0].InputStream : request.InputStream,
                ContentType = formUpload ? request.Files[0].ContentType : request.ContentType
            };

            return upload;
        }
    }
}