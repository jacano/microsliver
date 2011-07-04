// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Reflection;

namespace MicroSliver.Silverlight.Extentions
{
    public abstract class MicroSliverViewModelLocator
    {
        private static IIoC IoC;
        private string assemblyName;

        public void LoadIoC(IIoC ioc, Assembly ExecutingAssembly)
        {
            IoC = ioc;
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
