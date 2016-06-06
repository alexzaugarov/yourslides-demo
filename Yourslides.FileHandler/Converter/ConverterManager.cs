using System.Collections.Concurrent;
using System.Threading;
using Yourslides.Model;
using Yourslides.Service;

namespace Yourslides.FileHandler.Converter {
    public class ConverterManager : IConverterManager, IConverterService {
        private readonly ConcurrentQueue<ConversionTask> _queue = new ConcurrentQueue<ConversionTask>();
        private readonly object _lockObject = new object();
        private readonly IConverter _converter;
        private readonly IPresentationService _presentationService;

        public ConverterManager(IConverter converter, IPresentationService presentationService) {
            _converter = converter;
            _presentationService = presentationService;
            _converter.Error += ConverterOnError;
            _converter.Completed += ConverterOnCompleted;
        }

        public void Start() {
            while (true) {
                ConversionTask task;
                while (_queue.TryPeek(out task)) {
                    _converter.Convert(task);
                    _queue.TryDequeue(out task);
                }
                lock (_lockObject)
                    Monitor.Wait(_lockObject);
            }
        }

        public void Add(ConversionTask task) {
            _queue.Enqueue(task);
            lock (_lockObject) {
                Monitor.PulseAll(_lockObject);
            }
        }

        #region Converter event handlers
        private void ConverterOnError(object sender, ConverterErrorEventArgs args) {
            _presentationService.Delete(args.Presentation);
            _presentationService.Save();
        }
        private void ConverterOnCompleted(object sender, ConverterEventArgs args) {
            args.Presentation.Visibility = PresentationVisibility.All; 
            _presentationService.UpdateAfterConvertation(args.Presentation);
            _presentationService.Save();
        }
        #endregion
    }
}