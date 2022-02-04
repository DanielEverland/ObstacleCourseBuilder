using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Tile : MonoBehaviour
{
    [SerializeField] private Vector2Int _size = Vector2Int.one;

    public Vector2Int Size => _size;

    private Dictionary<string, string> data;

    public virtual void ApplyData(TileData tileData)
    {
        data = tileData.Data;
    }

    protected bool HasData(string key)
    {
        return data.ContainsKey(key);
    }

    protected T GetData<T>(string key)
    {
        if (data.ContainsKey(key))
            return JsonUtility.FromJson<T>(data[key]);

        Debug.LogError($"Attempted to get data with key {key}, but none exists!");
        return default;
    }
}
