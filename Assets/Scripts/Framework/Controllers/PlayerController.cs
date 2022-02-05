using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerController : Controller
{
    public static PlayerController Current;

    private readonly Dictionary<KeyCode, List<Action>> KeyDownDelegates = new Dictionary<KeyCode, List<Action>>();
    private readonly Dictionary<KeyCode, List<Action>> KeyUpDelegates = new Dictionary<KeyCode, List<Action>>();
    private readonly Dictionary<KeyCode, List<Action>> KeyContinuousDelegates = new Dictionary<KeyCode, List<Action>>();

    private void Awake()
    {
        Current = this;
    }

    public void BindToKeyDown(KeyCode key, Action callback)
    {
        if(!KeyDownDelegates.ContainsKey(key))
            KeyDownDelegates.Add(key, new List<Action>());
        
        KeyDownDelegates[key].Add(callback);
    }

    public void BindToKeyUp(KeyCode key, Action callback)
    {
        if(!KeyUpDelegates.ContainsKey(key))
            KeyUpDelegates.Add(key, new List<Action>());
        
        KeyUpDelegates[key].Add(callback);
    }
    
    public void BindToKeyContinuous(KeyCode key, Action callback)
    {
        if(!KeyContinuousDelegates.ContainsKey(key))
            KeyContinuousDelegates.Add(key, new List<Action>());
        
        KeyContinuousDelegates[key].Add(callback);
    }

    protected virtual void Update()
    {
        QueryDelegateDictionary(KeyDownDelegates, Input.GetKeyDown);
        QueryDelegateDictionary(KeyUpDelegates, Input.GetKeyUp);
        QueryDelegateDictionary(KeyContinuousDelegates, Input.GetKey);
    }

    private void QueryDelegateDictionary(Dictionary<KeyCode, List<Action>> delegates, Func<KeyCode, bool> predicate)
    {
        foreach (KeyValuePair<KeyCode, List<Action>> pair in delegates)
        {
            if (IsMouseKey(pair.Key) && IsMouseOverUI())
                continue;

            if (predicate.Invoke(pair.Key))
            {
                pair.Value.ForEach(x => x.Invoke());
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
