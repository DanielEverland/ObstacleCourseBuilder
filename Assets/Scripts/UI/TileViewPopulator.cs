using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class TileViewPopulator : MonoBehaviour
{
    [SerializeField] private RectTransform Content;
    [SerializeField] private TileViewEntry EntryPrefab;
    [SerializeField] private ProjectSettings ProjectSettings;
    [SerializeField] private ToggleGroup ToggleGroup;

    private void Start()
    {
        foreach (ProjectSettings.TileInfo tileInfo in ProjectSettings.TileTypes)
        {
            TileViewEntry tileViewEntry = Instantiate(EntryPrefab);
            tileViewEntry.transform.SetParent(Content);
            tileViewEntry.GetToggle().group = ToggleGroup;

            tileViewEntry.Initialize(tileInfo.TileObj.GetComponent<Tile>());
        }

        Content.GetComponentInChildren<Toggle>().isOn = true;
    }
}
