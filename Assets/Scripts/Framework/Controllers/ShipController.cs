using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : PlayerController
{
    [SerializeField] private ShipBuilder ShipBuilder;
    [SerializeField] private CameraFollow CameraFollow;

    private void Start()
    {
        ShipBuilder.LoadShip(ShipSerializer.DeserializeShip());
        CameraFollow.SetTarget(ShipBuilder.ShipObject);
    }
}
