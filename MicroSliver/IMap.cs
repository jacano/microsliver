// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public interface IMap
    {
        Type Concrete { get; }
        Scope Scope { get; }
        ICreator Creator { get; }

        void ToSingletonScope();
        void ToInstanceScope();
    }
}
