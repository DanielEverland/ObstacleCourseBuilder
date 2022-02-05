using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContextMenuCreator : MonoBehaviour
{
    [SerializeField] private ContextMenu ContextMenuPrefab;
    [SerializeField] private RectTransform ContextMenuParent;

    private const float TimeUntilShowMenu = 0.5f;
    private const float TimeUntilRemoveMenu = 0.5f;

    private IContextMenuHandler HoveredContextMenuHandler;
    private IContextMenuHandler SelectedContextMenuHandler;

    private ContextMenu VisibleContextMenu;
    private float HoverStartedTime;
    private float LostHoverStartedTime;
    private bool IsHover = false;

    void Update()
    {
        QueryHoveredMenuHandler();
        
        if (SelectedContextMenuHandler == null && HoveredContextMenuHandler != null && !IsHover)
        {
            IsHover = true;
            HoverStartedTime = Time.time;
        }


        if (SelectedContextMenuHandler == null && HoveredContextMenuHandler != null)
        {
            if(VisibleContextMenu == null)
                QueryAddNewContextMenu();
        }
        else if ((HoveredContextMenuHandler == null || HoveredContextMenuHandler != SelectedContextMenuHandler) && !EventSystem.current.IsPointerOverGameObject() && IsHover)
        {
            IsHover = false;
            LostHoverStartedTime = Time.time;
        }
        
        if (!IsHover && VisibleContextMenu != null)
        {
            QueryRemoveMenu();
        }
    }

    private void QueryHoveredMenuHandler()
    {
        Vector2 mousePosInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        Physics2D.Raycast(mousePosInWorld, Vector2.zero, new ContactFilter2D(), hits);
        
        foreach (RaycastHit2D hit in hits)
        {
            var menuHandler = hit.collider.GetComponent<IContextMenuHandler>();
            if(menuHandler != null)
            {
                HoveredContextMenuHandler = menuHandler;
                return;
            }
        }

        HoveredContextMenuHandler = null;
    }

    private void QueryAddNewContextMenu()
    {
        if (Time.time - HoverStartedTime >= TimeUntilShowMenu)
        {
            AddNewContextMenu();
        }
    }

    private void AddNewContextMenu()
    {
        SelectedContextMenuHandler = HoveredContextMenuHandler;

        VisibleContextMenu = Instantiate(ContextMenuPrefab);
        VisibleContextMenu.transform.SetParent(ContextMenuParent);
        VisibleContextMenu.GetComponent<RectTransform>().position = Input.mousePosition;

        SelectedContextMenuHandler.OnContextMenuCreated(VisibleContextMenu);
    }

    private void QueryRemoveMenu()
    {
        if (Time.time - LostHoverStartedTime >= TimeUntilRemoveMenu)
        {
            RemoveMenu();
        }
    }

    private void RemoveMenu()
    {
        SelectedContextMenuHandler = null;
        Destroy(VisibleContextMenu.gameObject);
    }
}
