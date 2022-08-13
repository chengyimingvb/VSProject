namespace Invoke
{
    internal class PoolNode {
        public object Value { get; set; }
        public PoolNode Next { get; set; }
    }
}