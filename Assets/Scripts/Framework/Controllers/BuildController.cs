using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : PlayerController
{
    [SerializeField] private ProjectSettings ProjectSettings;
    
    private void Awake()
    {
        BindToKeyDown(KeyCode.Mouse0, OnLeftMouseDown);
        BindToKeyDown(KeyCode.Mouse1, OnRightMouseDown);
    }

    private void OnLeftMouseDown()
    {
        InstantiateNewTile(ProjectSettings.TileTypes[0].ID);
    }

    private void OnRightMouseDown()
    {
        InstantiateNewTile(ProjectSettings.TileTypes[1].ID);
    }

    private void InstantiateNewTile(uint id)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int roundedPosition = Vector2Int.RoundToInt(worldPos);

        Tile newTile = ProjectSettings.InstantiateTile(id);
        newTile.gameObject.transform.position = (Vector2)roundedPosition;
    }
}
