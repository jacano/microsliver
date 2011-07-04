// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Reflection;

namespace MicroSliver.Silverlight.Extensions
{
    public abstract class MicroSliverViewModelLocator
    {
        private string assemblyName;
        private static IIoC _ioc;
        public IIoC IoC
        {
            get
            {
                return _ioc;
            }
        }

        public void LoadIoC(IIoC ioc, Assembly ExecutingAssembly)
        {
            _ioc = ioc;
            assemblyName = ExecutingAssembly.FullName;
        }

        public object this[string viewModel]
        {
            get
            {
                var fullname = viewModel + ", " + assemblyName;
                return IoC.GetByType(Type.GetType(fullname, true, true));
            }
        }
    }
}
