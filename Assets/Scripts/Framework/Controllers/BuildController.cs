using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BuildController : PlayerController, ITileViewSelectedInterface, IFlySelectedInterface
{
    [SerializeField] private ProjectSettings ProjectSettings;
    [SerializeField] private ShipBuilder ShipBuilder;
    [SerializeField] private SpriteRenderer GhostPrefab;
    
    private uint selectedTileType;

    private SpriteRenderer GhostObject;
    
    private void Awake()
    {
        BindToKeyDown(KeyCode.Mouse0, OnLeftMouseDown);
        GhostObject = Instantiate(GhostPrefab);
    }

    private void Start()
    {
        if (ShipSerializer.HasSavedShip())
        {
            ShipBuilder.LoadShip(ShipSerializer.DeserializeShip());
        }
    }

    protected override void Update()
    {
        base.Update();

        GhostObject.enabled = IsValidPosition();
        Vector3 ghostPosition = (Vector2) GetTilePosition();
        ghostPosition.z = -1.0f;
        GhostObject.transform.position = ghostPosition;
    }
    
    private void OnLeftMouseDown()
    {
        if(!IsHoveringOverExistingTile())
            InstantiateNewTile(selectedTileType);
    }

    private void InstantiateNewTile(uint id)
    {
        Vector2Int roundedPosition = GetTilePosition();

        TileData newTileData = new TileData();
        newTileData.X = roundedPosition.x;
        newTileData.Y = roundedPosition.y;
        newTileData.TileID = id;

        ShipBuilder.AddTile(newTileData);
    }

    public void OnSelected(uint tileID)
    {
        selectedTileType = tileID;

        GhostObject.sprite = ProjectSettings.GetTile(selectedTileType).GetComponent<SpriteRenderer>().sprite;
    }

    public void OnFlySelected()
    {
        ShipSerializer.SerializeShip(ShipBuilder.ShipData);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private Vector2Int GetTilePosition()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return Vector2Int.RoundToInt(worldPos);
    }

    private bool IsValidPosition()
    {
        return !EventSystem.current.IsPointerOverGameObject() && !IsHoveringOverExistingTile();
    }

    private bool IsHoveringOverExistingTile()
    {
        Vector2Int mousePosition = GetTilePosition();
        return ShipBuilder.ShipData.Tiles.Any(x => x.X == mousePosition.x && x.Y == mousePosition.y);
    }
}
