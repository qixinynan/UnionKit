using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnionSetting : EditorWindow
{

    [MenuItem("Window/Union")]
    public static void ShowWindow()
    {
        EditorWindow window = GetWindow(typeof(UnionSetting));
        window.titleContent = new GUIContent("Union");
    }

    private void OnGUI()
    {
        GUILayout.Space(20);
        if (GUILayout.Button("Open Manager Setting"))
        {
            ManagerSetting.ShowWindow();
        }
    }
}
