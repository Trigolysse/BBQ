using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: MonoBehaviour
{
    private string displayName;
    private Sprite icon;
    private ItemType type;

    public Item(string displayName, Sprite icon, ItemType type) {
        this.DisplayName = displayName;
        this.Icon = icon;
        this.Type = type;
    }

    public ItemType Type { get => type; set => type = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public string DisplayName { get => displayName; set => displayName = value; }
}

public static class Items
{
    static List<Item> items = new List<Item>
    {
        new Item("Wood", Resources.Load<Sprite>("Items/Wood"), ItemType.WOOD),
        new Item("Yellow flower", Resources.Load<Sprite>("Items/Yellow"), ItemType.YELLOW_FLOWER),
        new Item("Dirt", Resources.Load<Sprite>("Items/Wood"), ItemType.DIRT)
    };

    public static Item getItemWithType(ItemType type)
    {
        foreach (Item item in items)
        {
            if (item.Type == type)
                return item;
        }
        return new Item("", Resources.Load<Sprite>("Items/Yellow"), ItemType.NONE);
    }
}

    public enum ItemType // all block types we can have
{
    NONE,
    DIRT,
    WOOD,
    COBBLESTONE,
    LOG,
    PLANKS,
    CHEST,
    CRAFTING_TABLE,
    FURNACE,
    SAND,
    YELLOW_FLOWER
}