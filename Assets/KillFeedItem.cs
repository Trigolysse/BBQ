using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    public Text text;
    public Text text2;
    public RawImage icon;
    public Sprite[] icons;

    public void Setup(string killer, string victim, WeaponName weaponName)
    {
        text.text = "<b>" + killer + "</b>";
        text2.text = "<b>" + victim + "</b>";

        switch(weaponName)
        {
            case WeaponName.GRENADE: icon.texture = icons[0].texture;
                break;
        }
    }
}

