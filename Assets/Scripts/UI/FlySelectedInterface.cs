using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public interface IFlySelectedInterface : IEventSystemHandler
{
    void OnFlySelected();
}
