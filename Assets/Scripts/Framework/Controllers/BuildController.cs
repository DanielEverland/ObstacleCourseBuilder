using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : PlayerController, ITileViewSelectedInterface
{
    [SerializeField] private ProjectSettings ProjectSettings;
    [SerializeField] private ShipBuilder ShipBuilder;

    private uint selectedTileType;
    
    private void Awake()
    {
        BindToKeyDown(KeyCode.Mouse0, OnLeftMouseDown);
    }
    
    private void OnLeftMouseDown()
    {
        InstantiateNewTile(selectedTileType);
    }

    private void InstantiateNewTile(uint id)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int roundedPosition = Vector2Int.RoundToInt(worldPos);

        TileData newTileData = new TileData();
        newTileData.X = roundedPosition.x;
        newTileData.Y = roundedPosition.y;
        newTileData.TileID = id;

        ShipBuilder.AddTile(newTileData);
    }

    public void OnSelected(uint tileID)
    {
        selectedTileType = tileID;
    }
}
