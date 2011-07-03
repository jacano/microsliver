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
    public class MicroSliverViewModelLocator
    {
        private static IIoC IoC;
        private string assemblyName;

        public void LoadIoC(IIoC ioc, string ExecutingAssemblyName)
        {
            IoC = ioc;
            assemblyName = ExecutingAssemblyName;
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
