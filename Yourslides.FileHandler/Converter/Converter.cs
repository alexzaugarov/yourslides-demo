using System;
using System.Diagnostics;
using System.IO;
using Ghostscript.NET.Processor;
using Yourslides.FileHandler.GhostscriptHelpers;
using Yourslides.FileHandler.Tools;
using Yourslides.Model;
using Yourslides.Service;

namespace Yourslides.FileHandler.Converter {
    public class Converter : IConverter {
        private readonly IGhostscriptImageDeviceWrap _device;
        private readonly IImageProcessor _imageProcessor;
        private readonly IPresentationService _presentationService;
        private ConversionTask _currentTask;
        private int _totalPageCount;
        private readonly GhostscriptProcessorFactory _processorFactory;

        public event EventHandler<ConverterEventArgs> Started;
        public event EventHandler<ConverterProcessingEventArgs> Processing;
        public event EventHandler<ConverterEventArgs> Completed;
        public event EventHandler<ConverterErrorEventArgs> Error;

        public Converter(GhostscriptProcessorFactory processorFactory, IGhostscriptImageDeviceFactory deviceFactory, IImageProcessor imageProcessor, IPresentationService presentationService, GhostscriptProcessorFactory processorFactory1) {
            _imageProcessor = imageProcessor;
            _presentationService = presentationService;
            _processorFactory = processorFactory;
            _device = deviceFactory.CreateDevice(144);
        }
        public void Convert(ConversionTask task) {
            _currentTask = task;
            _device.OutputDir = task.OutputDir;
            _device.InputFile = task.InputFile;
            var processor = _processorFactory.CreateGhostscriptProcessor();
            processor.Processing += ProcessingHandler;
            processor.Error += ErrorHandler;
            try {
                OnStarted(_currentTask.Presentation);
                _imageProcessor.CreateSubdirectories(task.OutputDir);
                processor.Process(_device.Device);
                //Convert last slide
                _imageProcessor.Process(Path.Combine(task.OutputDir, _totalPageCount + _device.Extension));
                OnCompleted(_currentTask.Presentation);
            } catch (Exception e) {
                Debug.WriteLine(e.Message);
                try {
                    processor.Dispose();
                    Directory.Delete(task.OutputDir, true);
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
                OnError("Unknown error", _currentTask.Presentation);
            }
        }

        private void ErrorHandler(object sender, GhostscriptProcessorErrorEventArgs args) {
            //OnError(args.Message, _currentTask.Presentation);
            throw new Exception("Ошибка при обработке");
        }

        private void ProcessingHandler(object sender, GhostscriptProcessorProcessingEventArgs args) {
            _totalPageCount = args.TotalPages;
            //Parameter args.CurrentPage shows page index which will be processed
            //Send for slide image converter previous (args.CurrentPage - 1) page index
            if (args.CurrentPage > 1) {
                _imageProcessor.Process(Path.Combine(_currentTask.OutputDir, (args.CurrentPage - 1) + _device.Extension));
            }
            OnProcessing(args, _currentTask.Presentation);
        }
        private void OnStarted(Presentation presentation) {
            if (Started != null) {
                Started(this, new ConverterEventArgs {
                    Presentation = presentation
                });
            }
        }

        private void OnError(string message, Presentation presentation) {
            Debug.WriteLine(message);
            if (Error != null) {
                Error(this, new ConverterErrorEventArgs {
                    Message = message,
                    Presentation = presentation,
                    OwnerId = presentation.OwnerId
                });
            }
        }
        private void OnProcessing(GhostscriptProcessorProcessingEventArgs args, Presentation presentation) {
            if (Processing != null) {
                Processing(this, new ConverterProcessingEventArgs {
                    CurrentSlide = args.CurrentPage,
                    TotalSlides = args.TotalPages,
                    Presentation = presentation
                });
            }
        }
        private void OnCompleted(Presentation presentation) {
            presentation.SlideCount = _totalPageCount;
            if (Completed != null) {
                Completed(this, new ConverterEventArgs { Presentation = presentation });
            }
        }
    }
}