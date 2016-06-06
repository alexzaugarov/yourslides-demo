using System;

namespace Yourslides.FileHandler.Service {
    public class FileFormatNotSupportedException : Exception {
        public FileFormatNotSupportedException(string message) : base(message) {
        }
    }
}