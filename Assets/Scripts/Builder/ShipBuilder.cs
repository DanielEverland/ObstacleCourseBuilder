using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    [SerializeField] private bool EnablePhysics = true;
    [SerializeField] private GameObject ShipPrefab;
    [SerializeField] private ProjectSettings ProjectSettings;

    private ShipData shipData = new ShipData();
    private GameObject shipObject;

    private void Awake()
    {
        ResetShipObject();
    }

    public void LoadShip(ShipData shipData)
    {
        ResetShipObject();
        foreach (TileData tile in shipData.Tiles)
        {
            InstantiateTile(tile);
        }
    }

    public void AddTile(TileData tileData)
    {
        shipData.AddTile(tileData);
        InstantiateTile(tileData);
    }

    private void InstantiateTile(TileData tileData)
    {
        Tile tile = ProjectSettings.InstantiateTile(tileData.TileID);
        
        tile.gameObject.transform.SetParent(shipObject.transform);
        tile.gameObject.transform.localPosition = new Vector3(tileData.X, tileData.Y, 0);
        tile.gameObject.GetComponent<Rigidbody2D>().simulated = EnablePhysics;

        var joint = tile.AddComponent<FixedJoint2D>();
        joint.enableCollision = true;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
        joint.dampingRatio = 1.0f;
        joint.connectedBody = shipObject.GetComponent<Rigidbody2D>();
    }

    private void ResetShipObject()
    {
        Destroy(shipObject);
        shipObject = Instantiate(ShipPrefab);
        shipObject.GetComponent<Rigidbody2D>().simulated = EnablePhysics;
    }
}
