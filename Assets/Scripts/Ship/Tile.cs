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
    
    private TileData tileData = new TileData();

    private void OnValidate()
    {
        if(id == 0)
            id = (uint) (Random.Range(int.MinValue, int.MaxValue) + int.MinValue);
    }

    public virtual void ApplyData(TileData tileData)
    {
        this.tileData = tileData;
    }

    protected bool HasData(string key)
    {
        return tileData.Data.ContainsKey(key);
    }

    protected T GetData<T>(string key)
    {
        if (tileData.Data.ContainsKey(key))
            return JsonUtil.FromJson<T>(tileData.Data[key]);

        Debug.LogError($"Attempted to get data with key {key}, but none exists!");
        return default;
    }

    protected void SetData<T>(string key, T obj)
    {
        tileData.SetData(key, obj);
    }

    public uint GetID()
    {
        return id;
    }
}
