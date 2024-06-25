using UnityEditor;
using UnityEngine;

public class PlayerPrefsViewer : EditorWindow
{
    [MenuItem("Tools/PlayerPrefs Viewer")]
    public static void ShowWindow()
    {
        GetWindow<PlayerPrefsViewer>("PlayerPrefs Viewer").minSize = new Vector2(400, 500);
    }

    private Vector2 scrollPosition;

    private void OnGUI()
    {
        GUILayout.Label("PlayerPrefs Viewer", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        if (GUILayout.Button("Refresh PlayerPrefs"))
        {
            RefreshPlayerPrefs();
        }

        DisplayPlayerPrefs();

        GUILayout.EndScrollView();

        if (GUILayout.Button("Clear All PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save(); // Đảm bảo các thay đổi được lưu lại
        }
    }

    private void DisplayPlayerPrefs()
    {
        foreach (var key in PlayerPrefsKeys())
        {
            DisplayPlayerPref(key);
        }
    }

    private void DisplayPlayerPref(string key)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label(key, GUILayout.Width(250));
        
        // Hiển thị giá trị tùy thuộc vào loại dữ liệu lưu trữ
        if (PlayerPrefs.HasKey(key))
        {
            var value = PlayerPrefs.GetString(key, "N/A");  // Lấy giá trị chuỗi để hiển thị
            GUILayout.Label(value);
        }
        else
        {
            GUILayout.Label("N/A");
        }

        if (GUILayout.Button("Delete", GUILayout.Width(100)))
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
        }

        GUILayout.EndHorizontal();
    }

    // Giả định rằng có một logic để lưu trữ và truy xuất danh sách các khóa
    private static string[] PlayerPrefsKeys()
    {
        // Placeholder for actual keys retrieval logic
        return new string[] {"UserID", "UserData_key1", "UserData_key2"}; // Thêm các khóa bạn đã lưu
    }

    // Thêm một phương thức để làm mới danh sách các khóa khi cần
    private void RefreshPlayerPrefs()
    {
        // Logic cập nhật danh sách các khóa
    }
}
