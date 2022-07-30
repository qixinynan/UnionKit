using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManagerSetting : EditorWindow
{
    int selectedManagerIndex = 0;

    public static void ShowWindow()
    {

        GetWindow(typeof(ManagerSetting));
    }

    private void OnGUI()
    {
        List<Type> managerTypes = UnionManager.GetAllManagerTypes();
        List<string> managerNames = new List<string>();
        managerTypes.ForEach(type => managerNames.Add(type.Name));
        selectedManagerIndex = EditorGUILayout.Popup(selectedManagerIndex, managerNames.ToArray());

    }
}
