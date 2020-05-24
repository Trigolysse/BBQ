using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string name;
    public Sprite icon;
    public ItemType type;

    static Item yellowFlower;


    public Item(string name, Sprite icon, ItemType type)
    {
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
    SAND,
    YELLOW_FLOWER
}