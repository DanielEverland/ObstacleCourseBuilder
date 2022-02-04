using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileViewEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private Graphic TargetGraphic;
    [SerializeField] private Toggle Toggle;
    [Space]
    [SerializeField] private Color NormalColor;
    [SerializeField] private Color SelectedColor;

    private Tile tile;

    public Toggle GetToggle()
    {
        return Toggle;
    }

    private void Awake()
    {
        Toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void Initialize(Tile tile)
    {
        this.tile = tile;
        Text.text = tile.TileName;
    }

    private void OnToggleChanged(bool newValue)
    {
        TargetGraphic.color = newValue ? SelectedColor : NormalColor;

        ExecuteEvents.ExecuteHierarchy<ITileViewSelectedInterface>(gameObject, null, (handler, data) => { handler.OnSelected(tile.GetID()); });
    }
}
