using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    [SerializeField] private GameObject ShipPrefab;
    [SerializeField] private ProjectSettings ProjectSettings;

    private GameObject currentShip;

    public void LoadShip(ShipData shipData)
    {
        currentShip = Instantiate(ShipPrefab);

        foreach (TileData tile in shipData.Tiles)
        {
            AddTile(tile);
        }
    }

    private void AddTile(TileData tileData)
    {
        Tile tile = ProjectSettings.InstantiateTile(tileData.TileID);

    }

    private void ClearShip()
    {
        Destroy(currentShip);
    }
}
