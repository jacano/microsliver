// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public interface IIoC
    {
        IMap Map<TContract, TConcrete>();
        IMap Map<TContract>(ICreator creator);
        void UnMap<TContract>();
        void Clear();
        T Get<T>();
        object GetByType(Type T);
    }
}
