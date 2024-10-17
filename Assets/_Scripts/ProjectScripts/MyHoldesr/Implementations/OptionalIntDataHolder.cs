using PaleLuna.DataHolder.Dictionary;
using UnityEngine;

public class OptionalIntDataHolder : MonoBehaviour, IOptionalData<int>
{
    [SerializeField]
    private SerializedDictionary<int> _data;

    private DictionaryHolder<string, int> _dictionaryHolder;

    private void Start() =>
        _dictionaryHolder = _data.Convert();

    public int GetData(string key) => 
        _dictionaryHolder[key];
}
