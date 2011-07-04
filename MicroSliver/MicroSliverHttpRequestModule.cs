// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System.Web;

namespace MicroSliver
{
    public class PreApplicationStartCode
    {
        public static void Start()
        {
            Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(MicroSliverHttpRequestModule));
        }
    }

    public class MicroSliverHttpRequestModule : IHttpModule
    {
        private static IIoC IoC;
        public void Init(HttpApplication context)
        {
            context.EndRequest += new System.EventHandler(ClearRequests);
        }

        public static void ManageIoC(IIoC ioc)
        {
            IoC = ioc;
        }

        private static void ClearRequests(object sender, System.EventArgs e)
        {
            IoC.ClearRequests();
        }

        public void Dispose()
        {
        }
    }
}
