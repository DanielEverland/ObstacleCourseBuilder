using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FlyButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        ExecuteEvents.ExecuteHierarchy<IFlySelectedInterface>(gameObject, null, ((handler, data) => handler.OnFlySelected()));
    }

    private void OnValidate()
    {
        button = GetComponent<Button>();
    }
}
