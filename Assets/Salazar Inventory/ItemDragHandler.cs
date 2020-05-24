using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private ItemHoverHandler itemHoverHandler;

    void Awake()
    {
        itemHoverHandler = GetComponent<ItemHoverHandler>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(0, 0);
    }

}
