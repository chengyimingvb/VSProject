using System;
using System.Collections.Generic;

namespace Invoke {
    internal class PoolManager {

        private readonly Dictionary<Type, ISingleTypePool> poolTable = new Dictionary<Type, ISingleTypePool>();

        public void CreatePool<T>(Func<T> factory, PoolParams @params = default (PoolParams))
            where T : class {

            Type type = typeof (T);

            if (!poolTable.ContainsKey(type)) {
                poolTable.Add(typeof (T), new SingleTypePool<T>(@params, factory));
            }
            else {
                throw new InvalidOperationException(string.Format("A pool for type '{0}' already exists.", type.Name));
            }
        }

        public void DeletePool<T>()
            where T : class, new() {

            Type type = typeof(T);
            if (poolTable.ContainsKey(type)) {
                poolTable.Remove(type);
            }
            else {
                ThrowNoPoolFoundException(type);
            }
        }

        public T GetInstance<T>()
            where T : class  {

            return (T) GetPool<T>().GetPooledInstance();
        }

        public void ReturnInstance<T>(T instance)
            where T : class {

            GetPool<T>().ReturnPooledInstance(instance);
        }



        private ISingleTypePool GetPool<T>()
            where T : class {

            Type type = typeof (T);

            ISingleTypePool singleTypePool;
            if (!poolTable.TryGetValue(type, out singleTypePool)) {
                ThrowNoPoolFoundException(type);
            }

            return singleTypePool;
        }

        private void ThrowNoPoolFoundException(Type type) {
            throw new InvalidOperationException(string.Format("No pool found for type '{0}'.", type.Name));
        }
    }
}
