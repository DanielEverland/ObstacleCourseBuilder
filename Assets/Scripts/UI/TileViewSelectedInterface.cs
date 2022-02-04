using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ITileViewSelectedInterface : IEventSystemHandler
{
    void OnSelected(uint tileID);
}
