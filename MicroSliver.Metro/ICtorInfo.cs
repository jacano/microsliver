// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System.Reflection;

namespace MicroSliver
{
    /// <summary>
    /// Provides a summary regarding constructor information for a mapping.
    /// </summary>
    public interface ICtorInfo
    {
        ConstructorInfo Ctor { get; set; }
        ParameterInfo[] CtorParams { get; set; }
    }
}
