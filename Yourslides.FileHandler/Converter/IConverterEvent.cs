using System;

namespace Yourslides.FileHandler.Converter {
    public interface IConverterEvent {
        event EventHandler<ConverterEventArgs> Started;
        event EventHandler<ConverterProcessingEventArgs> Processing;
        event EventHandler<ConverterEventArgs> Completed;
        event EventHandler<ConverterErrorEventArgs> Error;  
    }
}