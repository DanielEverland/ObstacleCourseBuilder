using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrusterTile : Tile, IContextMenuHandler
{
    [SerializeField]
    private float Force;

    [SerializeField] private KeybindingMenu KeybindingConeMenuPrefab;

    private HashSet<KeyCode> BoundInput = new HashSet<KeyCode>();
    private const string InputKey = "input";
    private int keysHeld = 0;

    public override void ApplyData(TileData tileData)
    {
        base.ApplyData(tileData);

        if(HasData(InputKey) && PlayerController.Current != null)
        {
            List<int> inputIntegers = GetData<List<int>>(InputKey);
            BoundInput = inputIntegers.Select(x => (KeyCode)x).ToHashSet();

            foreach (KeyCode keyCode in BoundInput)
            {
                PlayerController.Current.BindToKeyDown(keyCode, () => keysHeld++);
                PlayerController.Current.BindToKeyUp(keyCode, () => keysHeld--);
            }
        }
    }

    private void FixedUpdate()
    {
        if (keysHeld > 0)
        {
            FireThruster();
        }
    }

    private void FireThruster()
    {
        Vector2 thrusterDirection = Renderer.transform.TransformDirection(0.0f, 1.0f, 0.0f);
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
        List<int> integerKeys = BoundInput.Select(x => (int)x).ToList();
        SetData(InputKey, integerKeys);
    }
}
