namespace Invoke
{
    internal interface ISingleTypePool {
        object GetPooledInstance();
        void ReturnPooledInstance(object instance);
        void Clear();
    }
}