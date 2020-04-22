using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillFeedItem : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Text text2;

    public void Setup(string killer, string victim)
    {
        text.text = "<b>" + killer + "</b>";
        text2.text = "<b>" + victim + "</b>";
    }
}
