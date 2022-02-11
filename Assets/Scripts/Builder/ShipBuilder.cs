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
        ResetShipObject(new ShipData());
    }

    public void LoadShip(ShipData shipData)
    {
        ResetShipObject(shipData);
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
        tile.ApplyData(tileData);
        
        tile.gameObject.transform.SetParent(ShipObject.transform);
        tile.gameObject.transform.localPosition = new Vector3(tileData.X, tileData.Y, 0);
    }

    private void ResetShipObject(ShipData shipData)
    {
        Destroy(ShipObject);
        ShipObject = Instantiate(ShipPrefab);

        Rigidbody2D shipRigidbody = ShipObject.GetComponent<Rigidbody2D>();
        shipRigidbody.constraints = EnablePhysics ? shipRigidbody.constraints : RigidbodyConstraints2D.FreezeAll;
        shipRigidbody.centerOfMass = GetCenterOfMass(shipData);
    }

    private Vector2 GetCenterOfMass(ShipData shipData)
    {
        Vector2 totalOffset = Vector2.zero;
        float totalMass = 0.0f;
        foreach (TileData tile in shipData.Tiles)
        {
            float tileMass = ProjectSettings.GetTile(tile.TileID).GetComponent<Tile>().Mass;
            totalOffset += new Vector2(tile.X, tile.Y) * tileMass;
            totalMass += tileMass;
        }
        
        return totalMass == 0.0f ? Vector2.zero : totalOffset / totalMass;
    }
}
