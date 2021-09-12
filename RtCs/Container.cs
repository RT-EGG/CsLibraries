namespace RtCs
{
    public interface IContainer<T>
    {
        T this[int inIndex] { get; }
        int Count { get; }
    }
}
