// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Rights Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public interface IMap
    {
        Type Concrete { get; set; }
        Scope Scope { get; set; }
        ICreator Creator { get; set; }
    }
}
