// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.ServiceModel.DomainServices.Server;

namespace MicroSliver.Web.Extensions
{
    public class MicroSliverDomainFactory : IDomainServiceFactory
    {
        private IIoC IoC;
        public MicroSliverDomainFactory(IIoC ioc)
        {
            IoC = ioc;
        }

        public DomainService CreateDomainService(Type domainServiceType, DomainServiceContext context)
        {
            DomainService domainService = (DomainService)IoC.GetByType(domainServiceType);

            if (domainService != null)
            {
                domainService.Initialize(context);
            }

            return domainService;
        }

        public void ReleaseDomainService(DomainService domainService)
        {
            domainService.Dispose();
        }
    }
}
