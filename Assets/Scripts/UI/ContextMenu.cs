using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    [SerializeField] private RectTransform Content;
    
    public void AddOption(GameObject option)
    {
        option.transform.SetParent(Content);
    }
}
