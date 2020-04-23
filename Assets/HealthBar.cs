using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviourPunCallbacks
{
	
    public float value;
    public Vector2 size = new Vector2(300, 30);
    public Texture2D emptyTex;
    GUIStyle style = new GUIStyle();

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
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.white);
        texture.wrapMode = TextureWrapMode.Repeat;
        texture.Apply();

        style.normal.background = texture;
        //draw the background:
        GUI.BeginGroup(new Rect(Screen.width / 2 - size.x / 2, Screen.height - size.y * 2, size.x, size.y));
        GUI.Box(new Rect(0, 0, size.x, size.y), emptyTex);

        //draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, size.x * value / 100, size.y));
        GUI.Box(new Rect(1, 1, size.x - 2, size.y - 2), new GUIContent(""), style);
        GUI.EndGroup();
        GUI.EndGroup();
    }
}
