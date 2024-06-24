using UnityEditor;
using UnityEngine;

public class PlayerPrefsViewer : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Viewer")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsViewer>("PlayerPrefs Viewer");
    }

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Viewer", EditorStyles.boldLabel);

        if (GUILayout.Button("Show All PlayerPrefs"))
        {
            foreach (var key in PlayerPrefsKeys())
            {
                GUILayout.Label($"{key}: {PlayerPrefs.GetString(key, "N/A")}");
            }
        }

        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private static string[] PlayerPrefsKeys()
    {
        // Placeholder for actual keys retrieval logic
        return new string[] {"UserID"}; 
    }
}
