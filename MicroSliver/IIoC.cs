// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Right Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public interface IIoC
    {
        void Map<TContract, TConcrete>();
        void Map<TContract>(ICreator creator);
        void MapToSingleton<TContract, TConcrete>();
        void MapToSingleton<TContract>(ICreator creator);
        void UnMap<TContract>();
        void Clear();
        T Get<T>();
        object GetByType(Type T);
    }
}
