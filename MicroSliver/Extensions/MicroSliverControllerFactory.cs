// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MicroSliver.Web.Extensions
{
    public class MicroSliverControllerFactory : DefaultControllerFactory
    {
        private IIoC IoC;
        public MicroSliverControllerFactory(IIoC ioc)
        {
            IoC = ioc;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            var controller = (IController)IoC.GetByType(controllerType);
            if (controller == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }

            return controller;
        }
    }
}
