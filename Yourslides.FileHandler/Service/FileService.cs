using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Yourslides.FileHandler.Converter;
using Yourslides.Model;
using Yourslides.Model.Api;
using Yourslides.Utils.Web;

namespace Yourslides.FileHandler.Service {
    public class FileService : IFileService {
        private readonly IConverterService _converterService;
        private readonly List<String> _supportedMimeTypes = new List<string> {
            ".pdf",
            ".ppt",
            ".pptx",
            ".odp"
        };

        public FileService(IConverterService converterService) {
            _converterService = converterService;
        }

        public void SaveFile(FilePresentation file, Presentation presentation) {
            if (!_supportedMimeTypes.Contains(Path.GetExtension(file.Filename))) {
                throw new FileFormatNotSupportedException("Неподдерживаемый формат файла");
            }
            var storageLocation = ConfigurationManager.AppSettings["PresentationStorageLocation"];
            var dirPath = Path.Combine(storageLocation, presentation.Id.ToString());
            var filePath = Path.Combine(dirPath, file.Filename);
            Directory.CreateDirectory(dirPath);
            file.SaveAs(filePath);
            _converterService.Add(new ConversionTask {
                OutputDir = dirPath,
                InputFile = filePath,
                Presentation = presentation
            });
        }

        public string GetSlide(long presentationId, int slideIndex, string quality) {
            string baseDir = Path.Combine(ConfigurationManager.AppSettings["PresentationStorageLocation"], presentationId.ToString());
            return quality == null ? Path.Combine(baseDir, slideIndex + ".png") : Path.Combine(baseDir, quality, slideIndex + ".png");
        }

        public void DeleteDirectory(string id) {
            Directory.Delete(Path.Combine(ConfigurationManager.AppSettings["PresentationStorageLocation"], id), true);
        }
    }
}