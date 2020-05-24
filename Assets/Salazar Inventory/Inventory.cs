using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Direction
{
    LEFT, RIGHT, DOWN, UP, NONE
}

public class Stack
{

    public Item item;  // the type of block
    public int count;       // the number of blocks in the stack

    public Stack(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }
}

public class Inventory : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Sprite slotSprite;
    private int slotSize = 32;

    public Sprite sp;
    public Sprite inventoryFrame;
    private bool showInventory = false;

    private Stack[,] inventory;
    private Item[,] armor;

    public Canvas inventoryCanvas;
    private GameObject go;
    private Player player;

    private void Awake()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        inventory = new Stack[4, 9];
        armor = new Item[4, 1];
        CreateCanvas();
        inventoryCanvas.enabled = false;
        AddInInventory(new Stack(new Item("Wood", sp, ItemType.WOOD), 64));
        AddInInventory(new Stack(Items.getItemWithType(ItemType.YELLOW_FLOWER), 1));
        AddInInventory(new Stack(Items.getItemWithType(ItemType.ORANGE_FLOWER), 1));
        AddInInventory(new Stack(Items.getItemWithType(ItemType.PURPLE_FLOWER), 1));
        AddInInventory(new Stack(Items.getItemWithType(ItemType.GOLD), 51));
    }

    // Update is called once per frame
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            //AddInInventory(new Stack(new Item("#</>d__Ss -o ssh key", sp, ItemType.DIRT), 32));
        }

        if(Input.GetKeyDown(KeyCode.L)) {
            inventoryCanvas.enabled = !inventoryCanvas.enabled;
            player.isOutOfFocus = !player.isOutOfFocus;
        }
    }

    private void CreateCanvas()
    {
        GameObject myGO = new GameObject();
        myGO.name = "InventoryCanvas";
        myGO.AddComponent<Canvas>();
        inventoryCanvas = myGO.GetComponent<Canvas>();
        inventoryCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        myGO.AddComponent<CanvasScaler>();
        myGO.AddComponent<GraphicRaycaster>();
        CreateFrameObject();
        CreateSlots();
    }

    private void CreateSlots()
    {
        for (int i = 0; i < armor.GetLength(0); i++)
            for (int j = 0; j < armor.GetLength(1); j++)
            {
                CreateGameObject($"Slot Armor {i} {j}", new Rect(j * slotSize + Screen.width / 2 - 4.5f * slotSize, i * slotSize + Screen.height / 2 - 140, slotSize, slotSize), slotSprite, true);
            }


        int width = inventory.GetLength(1) * slotSize;
        int height = inventory.GetLength(0) * slotSize;
        for (int i = 0; i < inventory.GetLength(0); i++)
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                if (i == inventory.GetLength(0) - 1)
                {
                    CreateGameObject($"Slot {i} {j}", new Rect(j * slotSize + Screen.width / 2 - width / 2, i * slotSize + Screen.height / 2  + 10, slotSize, slotSize), slotSprite, true);
                }
                else
                    CreateGameObject($"Slot {i} {j}", new Rect(j * slotSize + Screen.width / 2 - width / 2, i * slotSize + Screen.height / 2 , slotSize, slotSize), slotSprite, true);
            }
    }

    public void AddInInventory(Stack stack)
    {
        int nbOfFullStacks = stack.count / 64;
        int remainder = stack.count % 64;

        for (int i = 0; i < nbOfFullStacks; i++)
            Add(new Stack(stack.item, 64));
        if (remainder == 0) return;

        foreach (Stack slot in inventory)
        {
            if (slot == null) continue;
            if (slot.item.Type == stack.item.Type && slot.count < 64)
            {
                int sum = slot.count + remainder;
                if (sum > 64)
                {
                    slot.count = 64;
                    Add(new Stack(stack.item, sum % 64));
                    return;
                }
                slot.count += remainder;
                return;
            }
        }
        Add(new Stack(stack.item, remainder));
    }

    void Add(Stack stack)
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
            for (int j = 0; j < inventory.GetLength(1); j++)
            {
                if (inventory[i, j] == null)
                {
                    AddStackToSlot(stack, i, j);
                    inventory[i, j] = stack;
                    return;
                }                
            }
    }

    private void AddStackToSlot(Stack stack, int i, int j)
    {
        int padding = 8;

        GameObject slot = GameObject.Find($"Slot {i} {j}");
        GameObject stackGo = new GameObject();
        stackGo.name = stack.item.DisplayName;
        stackGo.AddComponent<ItemDragHandler>();
        //stackGo.AddComponent<ItemHoverHandler>();
        ItemHoverHandler.Instantiate(stackGo, stack);
        Image image = stackGo.AddComponent<Image>();
        image.sprite = stack.item.Icon;
        RectTransform r = stackGo.transform as RectTransform;
        r.sizeDelta = new Vector2(slotSize - padding, slotSize - padding);
        stackGo.transform.SetParent(slot.transform);
        image.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    void CreateFrameObject()
    {
        int width = 300;
        int height = 300;
        CreateGameObject("Frame", new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height), inventoryFrame);
        GameObject.Find("Frame").AddComponent<ItemDropHandler>();
    }

    public static void GUIDrawSprite(Rect rect, Sprite sprite)
    {
        Rect spriteRect = sprite.rect;
        Texture2D tex = sprite.texture;
        GUI.DrawTextureWithTexCoords(rect, tex, new Rect(spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
    }

    private void CreateGameObject(string name, Rect rect, Sprite sprite, bool slot = false)
    {
        GameObject go = new GameObject();
        go.name = name;
        if(slot)
            go.AddComponent<Slot>();
        Image imageGameObject = go.AddComponent<Image>();
        imageGameObject.sprite = sprite;
        RectTransform r = go.transform as RectTransform;
        r.sizeDelta = new Vector2(rect.width, rect.height);
        r.anchorMin = new Vector2(0, 1);
        r.anchorMax = new Vector2(0, 1);
        r.pivot = new Vector2(0, 1);

        go.transform.SetParent(inventoryCanvas.transform);
        imageGameObject.rectTransform.anchoredPosition = new Vector2(rect.x, -rect.y);
    }
}
