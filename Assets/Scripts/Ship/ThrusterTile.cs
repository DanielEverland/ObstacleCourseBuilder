using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterTile : Tile
{
    [SerializeField]
    private float Force;

    private void FixedUpdate()
    {
        Vector2 thrusterDirection = transform.TransformDirection(0.0f, 1.0f, 0.0f);
        gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(thrusterDirection * Force, transform.position);
    }
}
