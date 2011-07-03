// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Right Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System.Reflection;

namespace MicroSliver
{
    public class CtorInfo : ICtorInfo
    {
        public ConstructorInfo Ctor { get; set; }
        public ParameterInfo[] CtorParams { get; set; }

        public CtorInfo(ConstructorInfo ctor, ParameterInfo[] ctorParams)
        {
            Ctor = ctor;
            CtorParams = ctorParams;
        }
    }
}
