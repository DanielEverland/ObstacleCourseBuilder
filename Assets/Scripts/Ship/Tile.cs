using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public abstract class Tile : MonoBehaviour
{
    [SerializeField] public string TileName;
    [SerializeField] private Vector2Int _size = Vector2Int.one;
    [SerializeField, HideInInspector] private uint id = 0;

    public Vector2Int Size => _size;

    private Dictionary<string, string> data;

    private void OnValidate()
    {
        if(id == 0)
            id = (uint) (Random.Range(int.MinValue, int.MaxValue) + int.MinValue);
    }

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

    public uint GetID()
    {
        return id;
    }
}
