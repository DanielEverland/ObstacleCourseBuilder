using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData
{
    public byte TileID;
    public Dictionary<string, string> Data;

    // Will override data if key already exists
    public void SetData<T>(string key, T data)
    {
        if (Data.ContainsKey(key))
            Data.Remove(key);
        
        Data.Add(key, JsonUtil.ToJson(data));
    }

    public string ToJson()
    {
        return JsonUtil.ToJson(this);
    }

    public static TileData FromJson(string json)
    {
        return JsonUtil.FromJson<TileData>(json);
    }
}
