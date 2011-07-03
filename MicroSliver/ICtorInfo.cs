// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Right Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System.Reflection;

namespace MicroSliver
{
    public interface ICtorInfo
    {
        ConstructorInfo Ctor { get; set; }
        ParameterInfo[] CtorParams { get; set; }
    }
}
