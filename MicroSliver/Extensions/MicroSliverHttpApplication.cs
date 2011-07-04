﻿// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System.Web;

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
        }

        protected abstract IIoC LoadIIoC();
    }
}
