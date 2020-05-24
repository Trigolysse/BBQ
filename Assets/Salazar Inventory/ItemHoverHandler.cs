using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

    void Update()
    {
        if(isOver)
            DrawDialog();
    }

    private void DrawDialog()
    {
        Debug.Log("HOVER");
    }
    private void OnGUI()
    {
        //GUI.ModalWindow(0, new Rect(0, 0, 500, 500), GUI.WindowFunction., "window");
    }
}
