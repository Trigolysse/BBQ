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
        GUI.skin.box.normal.background = texture;

        GUI.Box(new Rect(Screen.width / 2 - gap - length, Screen.height / 2 - thickness / 2, length, thickness), GUIContent.none); //Left
        GUI.Box(new Rect(Screen.width / 2 + gap, Screen.height / 2 - thickness / 2, length, thickness), GUIContent.none); // Right
        GUI.Box(new Rect(Screen.width / 2 - thickness / 2, Screen.height / 2 - gap - length, thickness, length), GUIContent.none); // Top
        GUI.Box(new Rect(Screen.width / 2 - thickness / 2, Screen.height / 2 + gap, thickness, length), GUIContent.none); // Bottom
        
    }
}
