using System.Collections.Generic;

public class SearchResponse<T>
{
    private List<T> items;

    public T at(int index)
    {
        if(index < 0 || index >= items.Count) throw new System.IndexOutOfRangeException();
        else return items[index];
    }

    public SearchResponse(List<T> items)
    {
        this.items = items;
    }
}
