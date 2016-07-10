using System;

namespace Yourslides.Data.Infrastructure {
    public interface IDbFactory : IDisposable {
        /// <summary>
        /// Returns existing DbContext instance. Creates new if not exist.
        /// </summary>
        /// <returns>DbContext instance which represents connection to the main database</returns>
        DataStore Instance();
        /// <summary>
        /// Forced creation of new DbContext instance
        /// </summary>
        void Reset();
    }
}