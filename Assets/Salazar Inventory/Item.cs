using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id;
    string name;
    Sprite icon;

    public Item(int id, string name, Sprite icon)
    {
        this.id = id;
        this.name = name;
        this.icon = Resources.Load<Sprite>("Sprites/Items/" + name);
    }
}
