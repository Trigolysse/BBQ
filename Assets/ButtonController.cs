using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviourPunCallbacks, IPointerEnterHandler, IPointerExitHandler
{

    public Text text;
    private Button button;
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite highlightedSprite;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!button.interactable)
            return;

        text.color = Color.black;
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(515, 36);
        button.GetComponent<Image>().sprite = highlightedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        
        text.color = Color.white;
        button.GetComponent<RectTransform>().sizeDelta = new Vector2(500, 30);
        button.GetComponent<Image>().sprite = defaultSprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
}
