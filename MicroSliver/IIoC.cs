// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Collections.Generic;

namespace MicroSliver
{
    public interface IIoC
    {
        IMap Map<TContract, TConcrete>();
        IMap Map<TContract>(ICreator creator);
        IEnumerable<IMap> GetMappings();
        IMap GetMap<TContract>();
        void UnMap<TContract>();
        void Clear();
        void ClearRequests();
        T Get<T>();
        object GetByType(Type T);
    }
}
