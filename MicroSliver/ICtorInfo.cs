// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Rights Reserved
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
