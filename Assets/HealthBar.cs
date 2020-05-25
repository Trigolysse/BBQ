using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviourPunCallbacks
{
	
    public float value;
    public Vector2 size = new Vector2(400, 30);
    public Texture2D emptyTex;
    GUIStyle style = new GUIStyle();
    public float widthPercent = 20.8f;
   
    public void SetMaxHealth(int health)
	{
		value = health;
	}

    public void SetHealth(int health)
	{
		value = health;
	}

    private void Start()
    {
	    SetMaxHealth(100);
    }

    void OnGUI()
    {
        float width = Screen.width * widthPercent / 100;
        Texture2D texture = new Texture2D(1, 1);
        if (value > 50)
            texture.SetPixel(0, 0, Color.white);
        else if (value > 20)
            texture.SetPixel(0, 0, new Color32(255, 99, 71, 255));
        else
            texture.SetPixel(0, 0, Color.red);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();

        style.normal.background = texture;

        //draw the background:
        GUI.BeginGroup(new Rect(Screen.width / 2 - width / 2, Screen.height - size.y * 2, width, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);
        

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, width * value / 100, size.y));
        GUI.Box(new Rect(1, 1, width - 2, size.y - 2), new GUIContent(""), style);
        GUI.EndGroup();
        GUI.EndGroup();
    }
}
