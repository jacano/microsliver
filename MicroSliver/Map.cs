// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;

namespace MicroSliver
{
    public class Map : IMap
    {
        public Type Concrete { get; private set; }
        public Scope Scope { get; private set; }
        public ICreator Creator { get; private set; }

        public Map(Type concrete)
        {
            Concrete = concrete;
            Scope = Scope.Instance;
        }

        public Map(Type concrete, ICreator creator)
            : this(concrete)
        {
            Creator = creator;
        }

        public void ToSingletonScope()
        {
            Scope = MicroSliver.Scope.Singleton;
        }

        public void ToInstanceScope()
        {
            Scope = MicroSliver.Scope.Instance;
        }

        public void ToRequestScope()
        {
            Scope = MicroSliver.Scope.Request;
        }

    }
}
