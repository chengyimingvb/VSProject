

namespace Invoke
{

    internal class EmptyNodesPool {

        private PoolParams poolParams;
        private PoolNode head;
        private int count;

        public EmptyNodesPool(PoolParams poolParams) {

            this.poolParams = poolParams;

            for (int i = 0; i < poolParams.PreloadedInstances; i++) {
                Push(Create());
            }
        }

        public PoolNode GetNode() {
            return count > 0
                ? Pop()
                : Create();
        }

        public void ReturnNode(PoolNode node) {
            if (count < poolParams.Size || poolParams.Size < 0) {
                Push(node);
            }
        }

        public void Clear() {
            head = null;
            count = 0;
        }

        private PoolNode Create() {
            return new PoolNode();
        }

        private void Push(PoolNode node) {
            node.Next = head;
            head = node;
            count++;
        }

        private PoolNode Pop() {
            PoolNode node = head;
            head = head.Next;
            node.Next = null;

            count--;
            return node;
        }
    }
}