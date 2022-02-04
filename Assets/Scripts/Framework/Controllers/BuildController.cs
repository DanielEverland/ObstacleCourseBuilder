using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : PlayerController
{
    private void Awake()
    {
        BindToKeyDown(KeyCode.Mouse0, OnMouseDown);
    }

    private void OnMouseDown()
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log($"Mouse Down! World Position: {worldPos}");
    }
}
