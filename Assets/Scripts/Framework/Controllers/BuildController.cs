using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildController : PlayerController, ITileViewSelectedInterface, IFlySelectedInterface
{
    [SerializeField] private ProjectSettings ProjectSettings;
    [SerializeField] private ShipBuilder ShipBuilder;
    
    private uint selectedTileType;
    
    private void Awake()
    {
        BindToKeyDown(KeyCode.Mouse0, OnLeftMouseDown);
    }

    private void Start()
    {
        if (ShipSerializer.HasSavedShip())
        {
            ShipBuilder.LoadShip(ShipSerializer.DeserializeShip());
        }
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

    public void OnFlySelected()
    {
        ShipSerializer.SerializeShip(ShipBuilder.ShipData);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
