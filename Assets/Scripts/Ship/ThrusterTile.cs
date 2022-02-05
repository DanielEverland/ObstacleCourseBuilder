using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterTile : Tile, IContextMenuHandler
{
    [SerializeField]
    private float Force;

    [SerializeField] private KeybindingMenu KeybindingConeMenuPrefab;

    private HashSet<KeyCode> BoundInput = new HashSet<KeyCode>();

    private void FixedUpdate()
    {
        Vector2 thrusterDirection = transform.TransformDirection(0.0f, 1.0f, 0.0f);
        gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition(thrusterDirection * Force, transform.position);
    }

    public void OnContextMenuCreated(ContextMenu contextMenu)
    {
        KeybindingMenu menu = Instantiate(KeybindingConeMenuPrefab);
        menu.Initialize(BoundInput);

        contextMenu.AddOption(menu.gameObject);

        menu.AddBindingChangedDelegate(OnKeybindingChanged);
    }

    private void OnKeybindingChanged(KeyCode code, bool isEnabled)
    {
        if (isEnabled)
            BoundInput.Add(code);
        else
            BoundInput.Remove(code);

        SaveData();
    }

    private void SaveData()
    {
        
    }
}
