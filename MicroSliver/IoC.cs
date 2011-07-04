﻿// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroSliver
{

    public enum Scope
    {
        Instance,
        Singleton,
        Request
    }

    public class IoC : IIoC
    {

        #region Properties

        private readonly IDictionary<Type, IMap> _mappings;
        private readonly IDictionary<Type, ICtorInfo> _cachedCtors;
        private readonly IDictionary<Type, object> _cachedSingletons;
        private readonly IDictionary<Type, object> _cachedRequests;

        #endregion

        #region Public Methods

        public IoC()
        {
            _mappings = new Dictionary<Type, IMap>();
            _cachedCtors = new Dictionary<Type, ICtorInfo>();
            _cachedSingletons = new Dictionary<Type, object>();
            _cachedRequests = new Dictionary<Type, object>();

#if !SILVERLIGHT
            MicroSliverHttpRequestModule.ManageIoC(this);
#endif
        }

        public IMap Map<TContract, TConcrete>()
        {
            return AddMap<TContract, TConcrete>();
        }

        public IMap Map<TContract>(ICreator creator)
        {
            return AddMap<TContract>(creator);
        }

        public void UnMap<TContract>()
        {
            var contract = typeof(TContract);

            if (_mappings.ContainsKey(contract))
            {
                _mappings.Remove(contract);
            }
        }

        public void Clear()
        {
            _mappings.Clear();
            _cachedCtors.Clear();
            _cachedSingletons.Clear();
            _cachedRequests.Clear();
        }

        public void ClearRequests()
        {
            _cachedRequests.Clear();
        }

        public IEnumerable<IMap> GetMappings()
        {
            return _mappings.Values;
        }

        public IMap GetMap<TContract>()
        {
            var contract = typeof(TContract);
            if (_mappings.ContainsKey(contract))
            {
                return _mappings[contract];
            }
            return null;
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object GetByType(Type T)
        {
            return Get(T);
        }

        #endregion

        #region Infrastructure

        private IMap AddMap<TContract, TConcrete>()
        {
            var contract = typeof(TContract);
            var concrete = typeof(TConcrete);
            if (!_mappings.ContainsKey(contract))
            {
                _mappings[contract] = new Map(concrete);
            }
            return _mappings[contract];
        }

        private IMap AddMap<TContract>(ICreator creator)
        {
            var contract = typeof(TContract);
            if (!_mappings.ContainsKey(contract))
            {
                _mappings[contract] = new Map(null, creator);
            }
            return _mappings[contract];
        }

        private object Get(Type T)
        {
            if (T.IsInterface)
            {
                if (_mappings.ContainsKey(T))
                {
                    var map = _mappings[T];
                    return (ProcessScope(T, map));
                }
                else
                {
                    throw new Exception("MicroSliver is unable to map interface of type " + T.Name + ".");
                }
            }
            CacheCtorInfo(T);
            return ProcessCtor(_cachedCtors[T]);
        }

        private object ProcessScope(Type T, IMap map)
        {
            switch (map.Scope)
            {
                case Scope.Instance:
                    if (map.Creator != null)
                    {
                        return map.Creator.Create();
                    }
                    return Get(_mappings[T].Concrete);
                case Scope.Singleton:
                    if (!_cachedSingletons.ContainsKey(T))
                    {
                        if (map.Creator != null)
                        {
                            _cachedSingletons.Add(T, map.Creator.Create());
                        }
                        else
                        {
                            _cachedSingletons.Add(T, Get(_mappings[T].Concrete));
                        }
                    }
                    return _cachedSingletons[T];
                case Scope.Request:
                    if (!_cachedRequests.ContainsKey(T))
                    {
                        if (map.Creator != null)
                        {
                            _cachedRequests.Add(T, map.Creator.Create());
                        }
                        else
                        {
                            _cachedRequests.Add(T, Get(_mappings[T].Concrete));
                        }
                    }
                    return _cachedRequests[T];
                default:
                    return null;
            }
        }

        private void CacheCtorInfo(Type T)
        {
            if (!_cachedCtors.ContainsKey(T))
            {
                var concreteCtor = T.GetConstructors()[0];
                var concreteCtorParams = concreteCtor.GetParameters();
                _cachedCtors.Add(T, new CtorInfo(concreteCtor, concreteCtorParams));
            }
        }

        private object ProcessCtor(ICtorInfo ctorInfo)
        {
            var parms = from p in ctorInfo.CtorParams select Get(p.ParameterType);
            return ctorInfo.Ctor.Invoke(parms.ToArray());
        }

        #endregion

    }
}