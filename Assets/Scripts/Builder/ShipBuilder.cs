using Unity.VisualScripting;
using UnityEngine;

public class ShipBuilder : MonoBehaviour
{
    [SerializeField] private bool EnablePhysics = true;
    [SerializeField] private GameObject ShipPrefab;
    [SerializeField] private ProjectSettings ProjectSettings;

    public ShipData ShipData { get; private set; } = new ShipData();
    public GameObject ShipObject { get; private set; }


    private void Awake()
    {
        ResetShipObject();
    }

    public void LoadShip(ShipData shipData)
    {
        ResetShipObject();
        foreach (TileData tile in shipData.Tiles)
        {
            AddTile(tile);
        }
    }
    
    public void AddTile(TileData tileData)
    {
        ShipData.AddTile(tileData);
        InstantiateTile(tileData);
    }
    
    private void InstantiateTile(TileData tileData)
    {
        Tile tile = ProjectSettings.InstantiateTile(tileData.TileID);
        
        tile.gameObject.transform.SetParent(ShipObject.transform);
        tile.gameObject.transform.localPosition = new Vector3(tileData.X, tileData.Y, 0);
        if(!EnablePhysics)
            tile.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        var joint = tile.AddComponent<FixedJoint2D>();
        joint.enableCollision = true;
        joint.breakForce = Mathf.Infinity;
        joint.breakTorque = Mathf.Infinity;
        joint.dampingRatio = 1.0f;
        joint.connectedBody = ShipObject.GetComponent<Rigidbody2D>();
    }

    private void ResetShipObject()
    {
        Destroy(ShipObject);
        ShipObject = Instantiate(ShipPrefab);

        if(!EnablePhysics)
            ShipObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
