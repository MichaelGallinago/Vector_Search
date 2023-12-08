namespace VectorSearch;

internal class CustomPriorityQueue<T>
{
    private readonly List<Tuple<T, double>> _elements = [];

    public int Count => _elements.Count;
    public void Clear() => _elements.Clear();

    public void Enqueue(T item, double priority)
    {
        _elements.Add(Tuple.Create(item, priority));
    }

    public T Dequeue()
    {
        if (_elements.Count == 0)
            throw new InvalidOperationException("Priority queue is empty");

        _elements.Sort((x, y) => x.Item2.CompareTo(y.Item2));

        T item = _elements[0].Item1;
        _elements.RemoveAt(0);
        return item;
    }
}