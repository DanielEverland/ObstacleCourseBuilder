using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipData
{
    public List<TileData> Tiles = new List<TileData>();
    
    public bool ContainsTileAtPosition(TileData tileData)
    {
        return Tiles.Any(existing => (existing.X == tileData.X && existing.Y == tileData.Y));
    }

    public void AddTile(TileData tileData)
    {
        if (ContainsTileAtPosition(tileData))
            throw new ArgumentNullException($"A tile already exists at ({tileData.X}, {tileData.Y})");

        Tiles.Add(tileData);
    }
}
