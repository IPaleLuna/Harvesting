using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public abstract class Searcher<T>
{
    public abstract UniTask<List<T>> Search();
}
