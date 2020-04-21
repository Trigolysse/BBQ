using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawInventory : MonoBehaviour
{

    [SerializeField]
    private Sprite slotSprite;
    [SerializeField]
    private int length = 5;
    [SerializeField]
    private int height = 5;

    private int slotSize = 32;
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
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                Rect spriteRect = new Rect(i * slotSize + 200, j * slotSize + 200, slotSize, slotSize);
                Texture2D tex = slotSprite.texture;
                GUI.DrawTextureWithTexCoords(spriteRect, tex, new Rect(spriteRect.x / tex.width, spriteRect.y / tex.height, spriteRect.width / tex.width, spriteRect.height / tex.height));
            }
        }
    }
}
