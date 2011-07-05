//// 
//// Author: Jeremiah Billmann <jbillmann@gmail.com>
//// Copyright (c) 2011
//// 
//// Licensed under the Microsoft Public License (Ms-PL).
//// 
//using System;
//using System.Web.Mvc;
//using System.Web.Routing;

//namespace MicroSliver.Web.Extensions
//{
//    public class MicroSliverControllerFactory : IControllerFactory
//    {
//        private IIoC IoC;

//        public MicroSliverControllerFactory(IIoC ioc)
//        {
//            IoC = ioc;
//        }

//        public IController CreateController(RequestContext context, Type controllerType)
//        {
//            return (IController)IoC.GetByType(controllerType);
//        }
//    }
//}
