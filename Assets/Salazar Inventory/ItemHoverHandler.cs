using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Stack stack;
    public static ItemHoverHandler Instantiate(GameObject where, Stack stack)
    {
        ItemHoverHandler myC = where.AddComponent<ItemHoverHandler>();
        myC.stack = stack;
        return myC;
    }
  
    public bool isOver = false;
    GameObject newGO;
    GameObject dialogFrame;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
        CreateFrameObject();

        newGO = new GameObject("KumO59bB907jlVI");
        Text myText = newGO.AddComponent<Text>();
        myText.fontSize = 14;
        myText.font = Resources.Load<Font>("Fonts/1_Minecraft-Regular");
        myText.horizontalOverflow = HorizontalWrapMode.Overflow;
        myText.text = $"<b>{stack.item.DisplayName} (#00{(int) Random.Range(1000, 9999)})</b>" +
            $"\n<color=yellow>{stack.count}</color>" +
            $"\n<b><i><color=#3c44a9>{stack.item.Type}</color></i></b>" +
            $"\n<i><color=grey>ft: {stack.item.DisplayName.ToLower()}_{stack.item.Type.ToString().ToLower()}</color></i>";
        myText.alignment = TextAnchor.UpperLeft;
        setDialogWidth((int)myText.preferredWidth);


        newGO.transform.SetParent(dialogFrame.transform);
        RectTransform r = newGO.transform as RectTransform;
        r.sizeDelta = new Vector2(130, 85);
        r.anchorMin = new Vector2(0, 1);
        r.anchorMax = new Vector2(0, 1);
        r.pivot = new Vector2(0, 1);
        myText.rectTransform.anchoredPosition = new Vector2(10, -5);
    }

    void setDialogWidth(int width)
    {
        RectTransform r = dialogFrame.transform as RectTransform;
        r.sizeDelta = new Vector2(width + 20, 80);
    }

    void CreateFrameObject()
    {
        dialogFrame = new GameObject("f9IrSR8ppscRQNs");
        Image imageGameObject = dialogFrame.AddComponent<Image>();
        imageGameObject.sprite = Resources.Load<Sprite>("Frame/Dialog");
        imageGameObject.color = Color.black;

        var tempColor = imageGameObject.color;
        tempColor.a = 0.9f;
        imageGameObject.color = tempColor;

        RectTransform r = dialogFrame.transform as RectTransform;
        r.sizeDelta = new Vector2(140, 80);
        r.anchorMin = new Vector2(0.5f, 0.5f);
        r.anchorMax = new Vector2(0.5f, 0.5f);
        r.pivot = new Vector2(0.5f, 0.5f);

        dialogFrame.transform.SetParent(GameObject.Find("InventoryCanvas").transform);
        imageGameObject.rectTransform.anchoredPosition = new Vector2(-227, 70);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        GameObject.Destroy(newGO);
        GameObject.Destroy(dialogFrame);
    }

    void Update()
    {
        if(isOver)
            DrawDialog();
    }

    private void DrawDialog()
    {
    }


}
