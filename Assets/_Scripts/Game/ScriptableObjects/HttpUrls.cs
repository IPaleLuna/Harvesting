using NaughtyAttributes;
using PaleLuna.DataHolder.Dictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "HttpParams", menuName = "Configs/Http")]
public class HttpUrls : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<string> _urls;

    public DictionaryHolder<string, string> urlsDic => _urls.Convert();
}
