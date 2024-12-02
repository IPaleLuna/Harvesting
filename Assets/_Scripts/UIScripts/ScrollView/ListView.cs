using System.Collections.Generic;
using UnityEngine;

public class ListView : MonoBehaviour
{
    [SerializeField]
    private ListItem itemPrefab;
    
    private List<ListItem> _items;
    private readonly List<ListItem> _activeItems = new();
    
    public List<ListItem> activeItems => _activeItems;

    public void Init()
    {
        _items = new(transform.GetComponentsInChildren<ListItem>());
        DisableAll();
    }
    
    public void Refresh(int itemCount)
    {
        DisableAll();

        for(int i = 0; i < itemCount; i++)
        {
            if (i >= _items.Count)
                _items.Add(Instantiate(itemPrefab, this.transform));

            _items[i].gameObject.SetActive(true);
            _activeItems.Add(_items[i]);
        }
    }

    private void DisableAll()
    {
        _items.ForEach(item => item.gameObject.SetActive(false));
        _activeItems?.Clear();
    }
}
