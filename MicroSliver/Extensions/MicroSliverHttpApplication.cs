// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Web;

namespace MicroSliver.Web.Extensions
{
    public abstract class MicroSliverHttpApplication : HttpApplication
    {
        private static IIoC _ioc;
        public IIoC IoC
        {
            get
            {
                return _ioc;
            }
        }

        public void LoadIoC(IIoC ioc)
        {
            _ioc = ioc;
        }
    }
}
