using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour
{
    [SerializeField] private Dictionary<Vector2Int, TileData> Tiles;
}
