using System;

namespace Yourslides.Data.Infrastructure {
    public interface IDbFactory : IDisposable {
        DataStore Init();
    }
}