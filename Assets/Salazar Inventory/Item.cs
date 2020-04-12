using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public Sprite icon;
    public ItemType type;

    public Item(int id, string name, Sprite icon, ItemType type)
    {
        this.id = id;
        this.name = name;
        this.icon = icon;
        this.type = type;
    }
}

public enum ItemType // all block types we can have
{
    NONE,
    DIRT,
    COBBLESTONE,
    LOG,
    PLANKS,
    CHEST,
    CRAFTING_TABLE,
    FURNACE,
    SAND
}