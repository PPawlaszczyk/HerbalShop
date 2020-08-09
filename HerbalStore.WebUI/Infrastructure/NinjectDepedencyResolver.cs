using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using Moq;
using HerbalStore.Domain.Abstract;
using HerbalStore.Domain.Entities;
using HerbalStore.Domain.Concrete;

namespace HerbalStore.WebUI.Infrastructure
{
    public class NinjectDepedencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepedencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices (Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private  void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}