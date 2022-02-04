using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : PlayerController
{
    private void Awake()
    {
        BindToKeyContinuous(KeyCode.D, OnPressedRight);
        BindToKeyContinuous(KeyCode.RightArrow, OnPressedRight);

        BindToKeyContinuous(KeyCode.A, OnPressedLeft);
        BindToKeyContinuous(KeyCode.LeftArrow, OnPressedLeft);

        BindToKeyContinuous(KeyCode.W, OnPressedUp);
        BindToKeyContinuous(KeyCode.UpArrow, OnPressedUp);

        BindToKeyContinuous(KeyCode.S, OnPressedDown);
        BindToKeyContinuous(KeyCode.DownArrow, OnPressedDown);
    }

    private void OnPressedRight()
    {
        Debug.Log("Right");
    }

    private void OnPressedLeft()
    {
        Debug.Log("Left");
    }

    private void OnPressedUp()
    {
        Debug.Log("Forward");
    }

    private void OnPressedDown()
    {
        Debug.Log("Backward");
    }
}
