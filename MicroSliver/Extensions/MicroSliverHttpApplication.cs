﻿// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Web;
using System.Web.Mvc;

namespace MicroSliver.Web.Extensions
{
    public abstract class MicroSliverHttpApplication : HttpApplication
    {
        private static IIoC _ioc;
        public static IIoC IoC
        {
            get
            {
                return _ioc;
            }
        }

        public override void Init()
        {
            _ioc = LoadIIoC();
            ControllerBuilder.Current.SetControllerFactory(new MicroSliverControllerFactory(_ioc));
        }

        protected abstract IIoC LoadIIoC();
        protected abstract void Application_Start();

        protected void Application_Start(object sender, EventArgs e)
        {
            Application_Start();
        }

    }


}