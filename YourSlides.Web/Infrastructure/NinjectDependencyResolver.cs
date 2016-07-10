using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Syntax;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace YourSlides.Web.Infrastructure {
    public class NinjectDependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel) {
            _kernel = kernel;
        }

        public object GetService(Type serviceType) {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _kernel.GetAll(serviceType);
        }

        public IDependencyScope BeginScope() {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }

        public void Dispose() {
            
        }
    }
    public class NinjectDependencyScope : IDependencyScope {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver) {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose() {
            IDisposable disposable = resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            resolver = null;
        }

        public object GetService(Type serviceType) {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            if (resolver == null)
                throw new ObjectDisposedException("this", "This scope has already been disposed");

            return resolver.GetAll(serviceType);
        }
    }

}