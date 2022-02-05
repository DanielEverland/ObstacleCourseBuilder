 using System.Collections;
using System.Collections.Generic;
 using UnityEditor;
 using UnityEngine;
 using UnityEngine.UI;

 [RequireComponent(typeof(Toggle))]
public class KeybindingMenuEntry : MonoBehaviour
{
    public KeybindingMenu Menu;
    
    [SerializeField] private Text text;
    [SerializeField] Toggle toggle;
    private KeyCode key;

    public void SetEnabled(bool isEnabled)
    {
        toggle.isOn = isEnabled;
    }

    public void Initialize(KeyCode key)
    {
        this.key = key;
        text.text = key.ToString();
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool isToggled)
    {
        Menu.OnKeyChanged(key, isToggled);
    }

    private void OnValidate()
    {
        toggle = GetComponent<Toggle>();
    }
}
