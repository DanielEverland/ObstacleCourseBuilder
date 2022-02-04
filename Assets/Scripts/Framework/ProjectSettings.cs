using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectSettings.asset", menuName = "Obstacle Course/Create Project Settings", order = 0)]
public class ProjectSettings : ScriptableObject
{
    [SerializeField]
    private List<GameObject> TileTypes = new List<GameObject>();

    public Tile GetTile(byte id)
    {
        if (id >= TileTypes.Count)
        {
            Debug.LogError($"Tile ID {id} does not exist!");
            return null;
        }

        return TileTypes[id].GetComponent<Tile>();
    }

    public Tile InstantiateTile(byte id)
    {
        Tile defaultObj = GetTile(id);
        GameObject instantiated = Instantiate<GameObject>(defaultObj.gameObject);
        return instantiated.GetComponent<Tile>();
    }
}
