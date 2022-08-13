using System.Collections.Generic;

namespace Invoke
{
    internal static class SuperInvokePoolHolder {

        private static readonly PoolParams DEFAULT_POOL_PARAMS = new PoolParams() {Size = -1};
        
        public static PoolManager PoolManager { get; set; }

        static SuperInvokePoolHolder() {
            PoolManager = new PoolManager();

            PoolManager.CreatePool<SingleTask>(()=>new SingleTask(), DEFAULT_POOL_PARAMS);
            PoolManager.CreatePool<Sequence>(()=>new Sequence(), DEFAULT_POOL_PARAMS);
            PoolManager.CreatePool<LinkedListNode<SingleTask>>(() => new LinkedListNode<SingleTask>(null), DEFAULT_POOL_PARAMS);
            PoolManager.CreatePool<LinkedListNode<ISuperInvokeRunnable>>(()=>new LinkedListNode<ISuperInvokeRunnable>(null), DEFAULT_POOL_PARAMS);
        }
    }
}
