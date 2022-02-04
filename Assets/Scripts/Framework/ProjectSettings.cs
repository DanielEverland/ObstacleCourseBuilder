using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectSettings.asset", menuName = "Obstacle Course/Create Project Settings", order = 0)]
public class ProjectSettings : ScriptableObject
{
    [System.Serializable]
    public class TileInfo
    {
        public uint ID = 0;
        public GameObject TileObj;
    }

    [SerializeField]
    public List<TileInfo> TileTypes = new List<TileInfo>();
    
    public Tile GetTile(uint id)
    {
        foreach (TileInfo tileEntry in TileTypes)
        {
            if (tileEntry.ID == id)
                return tileEntry.TileObj.GetComponent<Tile>();
        }

        throw new KeyNotFoundException($"Tile ID {id} does not exist!");
    }

    public Tile InstantiateTile(uint id)
    {
        Tile defaultObj = GetTile(id);
        GameObject instantiated = Instantiate(defaultObj.gameObject);
        return instantiated.GetComponent<Tile>();
    }

    private void OnValidate()
    {
        for (int i = 0; i < TileTypes.Count; i++)
        {
            TileInfo info = TileTypes[i];
            if (info.TileObj != null)
                TileTypes[i].ID = info.TileObj.GetComponent<Tile>().GetID();
        }
    }
}
