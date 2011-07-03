// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Right Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public class Map : IMap
    {
        public Type Concrete { get; set; }
        public Scope Scope { get; set; }
        public ICreator Creator { get; set; }

        public Map(Type concrete, Scope scope)
        {
            Concrete = concrete;
            Scope = scope;
        }

        public Map(Type concrete, Scope scope, ICreator creator)
            : this(concrete, scope)
        {
            Creator = creator;
        }
    }
}
