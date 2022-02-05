using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KeybindingMenu : MonoBehaviour
{
    [SerializeField] private KeybindingMenuEntry EntryPrefab;
    [SerializeField] private RectTransform Content;

    private Action<KeyCode, bool> callback;

    private Dictionary<KeyCode, KeybindingMenuEntry> usedKeys = new Dictionary<KeyCode, KeybindingMenuEntry>()
    {
        { KeyCode.W, null },
        { KeyCode.A, null },
        { KeyCode.S, null },
        { KeyCode.D, null },
        { KeyCode.Q, null },
        { KeyCode.R, null },
        { KeyCode.Z, null },
        { KeyCode.X, null },
        { KeyCode.C, null },
    };

    public void Initialize(HashSet<KeyCode> boundKeys)
    {
        var keys = new List<KeyCode>(usedKeys.Keys.ToList());
        foreach (KeyCode key in keys)
        {
            KeybindingMenuEntry newEntry = Instantiate(EntryPrefab);
            newEntry.transform.SetParent(Content);
            newEntry.SetEnabled(boundKeys.Contains(key));

            newEntry.Menu = this;
            newEntry.Initialize(key);

            usedKeys[key] = newEntry;
        }
    }

    public void AddBindingChangedDelegate(Action<KeyCode, bool> keyChangedDelegate)
    {
        callback = keyChangedDelegate;
    }

    public void OnKeyChanged(KeyCode code, bool isEnabled)
    {
        callback(code, isEnabled);
    }
}
