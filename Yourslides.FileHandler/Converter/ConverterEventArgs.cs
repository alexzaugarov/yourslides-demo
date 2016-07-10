using System;
using Yourslides.Model;

namespace Yourslides.FileHandler.Converter {
    public class ConverterEventArgs : EventArgs {
        public Presentation Presentation { get; set; }
    }
    public class ConverterProcessingEventArgs : ConverterEventArgs {
        public int CurrentSlide { get; set; }
        public int TotalSlides { get; set; }
    }
    public class ConverterErrorEventArgs : ConverterEventArgs {
        public string Message { get; set; }
        public string OwnerId { get; set; }
    }
}