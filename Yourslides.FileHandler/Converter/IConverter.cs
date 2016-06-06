namespace Yourslides.FileHandler.Converter {
    public interface IConverter : IConverterEvent {
        void Convert(ConversionTask task);
    }
}