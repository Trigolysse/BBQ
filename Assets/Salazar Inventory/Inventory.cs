using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Sprite slotSprite;
    private int slotSize = 32;

    [SerializeField]
    public List<Item> items = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DrawInventory();
        }
    }

    void OpenInventory()
    {

    }

    void DrawInventory()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GUIDrawSprite(new Rect(i * slotSize + 200, j * slotSize + 200, slotSize, slotSize), slotSprite);
            }
        }
    }

    public static void GUIDrawSprite(Rect rect, Sprite sprite)
    {
        Rect spriteRect = sprite.rect;
        Texture2D tex = sprite.texture;
        GUI.DrawTextureWithTexCoords(rect, tex, new Rect(spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
    }
}
