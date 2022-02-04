using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerController : Controller
{
    private readonly Dictionary<KeyCode, Action> KeyDownDelegates = new Dictionary<KeyCode, Action>();
    private readonly Dictionary<KeyCode, Action> KeyUpDelegates = new Dictionary<KeyCode, Action>();
    private readonly Dictionary<KeyCode, Action> KeyContinuousDelegates = new Dictionary<KeyCode, Action>();
    
    protected void BindToKeyDown(KeyCode key, Action callback)
    {
        KeyDownDelegates.Add(key, callback);
    }

    protected void BindToKeyUp(KeyCode key, Action callback)
    {
        KeyUpDelegates.Add(key, callback);
    }
    
    protected void BindToKeyContinuous(KeyCode key, Action callback)
    {
        KeyContinuousDelegates.Add(key, callback);
    }

    private void Update()
    {
        QueryDelegateDictionary(KeyDownDelegates, Input.GetKeyDown);
        QueryDelegateDictionary(KeyUpDelegates, Input.GetKeyUp);
        QueryDelegateDictionary(KeyContinuousDelegates, Input.GetKey);
    }

    private void QueryDelegateDictionary(Dictionary<KeyCode, Action> delegates, Func<KeyCode, bool> predicate)
    {
        foreach (KeyValuePair<KeyCode, Action> pair in delegates)
        {
            if (IsMouseKey(pair.Key) && IsMouseOverUI())
                continue;

            if (predicate.Invoke(pair.Key))
            {
                pair.Value.Invoke();
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool IsMouseKey(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Mouse0:
            case KeyCode.Mouse1:
            case KeyCode.Mouse2:
            case KeyCode.Mouse3:
            case KeyCode.Mouse4:
            case KeyCode.Mouse5:
            case KeyCode.Mouse6:
                return true;
            default:
                return false;
        }
    }
}
