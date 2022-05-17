using System;

[Serializable]
public class T<T1, T2> 
{
    public T1 Item1;
    public T2 Item2;

    public T()
    {
    }

    public T(T1 Item1, T2 Item2)
    {
        this.Item1 = Item1;
        this.Item2 = Item2;
    }

    public static implicit operator T<T1,T2>(Tuple<T1,T2> tuple) => new T<T1, T2>(tuple.Item1, tuple.Item2);
}
