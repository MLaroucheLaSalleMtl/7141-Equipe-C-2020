using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WarningCreationWindow : EditorWindow
{
    static WarningCreationWindow window;

    public static void OpenWindow()
    {        
        window = (WarningCreationWindow)GetWindow(typeof(WarningCreationWindow));
        window.minSize = new Vector2(500, 100);
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("IL FAUT ETRE CERTAIN D'AVOIR BIEN FINI AVANT DE CREER LES ASSETS.");
        if(GUILayout.Button("CREATE EVERYTHING."))
        {
            GetWindow<DungeonEventMakerWindow>().CreateEverything();
        }
    }
}
