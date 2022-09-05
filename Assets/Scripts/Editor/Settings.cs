using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AsteroidOrbit))]
public class Settings : EditorWindow
{ 
	[MenuItem("Window/Settings")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(Settings));
    }

    private void OnGUI()
    {
		
	}
}
