// 
// Author: Jeremiah Billmann <jbillmann@gmail.com>
// Copyright (c) 2011 All Rights Reserved
// 
// Licensed under the Microsoft Public License (Ms-PL).
// 
using System;
using System.Collections.Generic;

namespace MicroSliver
{

    public enum Scope
    {
        Instance,
        Singleton
    }

    public class IoC : IIoC
    {

        #region Properties

        private readonly IDictionary<Type, IMap> _mappings;
        private readonly IDictionary<Type, ICtorInfo> _cachedCtors;
        private readonly IDictionary<Type, object> _cachedSingletons;

        #endregion

        #region Public Methods

        public IoC()
        {
            _mappings = new Dictionary<Type, IMap>();
            _cachedCtors = new Dictionary<Type, ICtorInfo>();
            _cachedSingletons = new Dictionary<Type, object>();
        }

        public void Map<TContract, TConcrete>()
        {
            AddMap<TContract, TConcrete>(Scope.Instance);
        }

        public void Map<TContract>(ICreator creator)
        {
            AddMap<TContract>(Scope.Instance, creator);
        }

        public void MapToSingleton<TContract, TConcrete>()
        {
            AddMap<TContract, TConcrete>(Scope.Singleton);
        }

        public void MapToSingleton<TContract>(ICreator creator)
        {
            AddMap<TContract>(Scope.Singleton, creator);
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

        private void AddMap<TContract, TConcrete>(Scope scope)
        {
            var contract = typeof(TContract);
            var concrete = typeof(TConcrete);
            if (!_mappings.ContainsKey(contract))
            {
                _mappings[contract] = new Map(concrete, scope);
            }
        }

        private void AddMap<TContract>(Scope scope, ICreator creator)
        {
            var contract = typeof(TContract);
            if (!_mappings.ContainsKey(contract))
            {
                _mappings[contract] = new Map(null, scope, creator);
            }
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
            var ctorInfo = _cachedCtors[T];

            if (ctorInfo.CtorParams.Length == 0)
            {
                return Activator.CreateInstance(T);
            }

            List<object> parameters = new List<object>(ctorInfo.CtorParams.Length);

            foreach (var param in ctorInfo.CtorParams)
            {
                parameters.Add(Get(param.ParameterType));
            }

            return ctorInfo.Ctor.Invoke(parameters.ToArray());
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

        #endregion

    }
}