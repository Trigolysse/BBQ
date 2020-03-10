using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float length;
    public float thickness;
    public float gap;

    void OnGUI()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.green);
        texture.Apply();

        GUI.DrawTexture(new Rect(Screen.width / 2 - gap - length, Screen.height / 2 - thickness / 2, length, thickness), texture); //Left
        GUI.DrawTexture(new Rect(Screen.width / 2 + gap, Screen.height / 2 - thickness / 2, length, thickness), texture); // Right
        GUI.DrawTexture(new Rect(Screen.width / 2 - thickness / 2, Screen.height / 2 - gap - length, thickness, length), texture); // Top
        GUI.DrawTexture(new Rect(Screen.width / 2 - thickness / 2, Screen.height / 2 + gap, thickness, length), texture); // Bottom
        
    }
}
