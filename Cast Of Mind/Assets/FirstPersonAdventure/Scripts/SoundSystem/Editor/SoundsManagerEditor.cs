using UnityEngine;
using UnityEditor;

public class SoundsManagerEditor : EditorWindow
{
    private SoundsManager manager;
    private Color defaultColor;

    [MenuItem("Adventure/Sounds")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SoundsManagerEditor>();
    }

    void OnGUI()
    {
        defaultColor = GUI.backgroundColor;
        if (SoundsManager.Instance == null) return;
        manager = SoundsManager.Instance;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) manager.AddNewItem();
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(manager.gameObject, PrefabUtility.GetPrefabParent(manager.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(manager.gameObject);
        EditorGUILayout.EndHorizontal();

        for (int i = manager.Items.Count - 1; i >= 0; i--)
        {
            SoundItem item = manager.Items[i];

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            item.id = EditorGUILayout.TextField(item.id, GUILayout.Width(120));
            EditorGUILayout.LabelField("Clip", GUILayout.Width(50));
            item.clip = (AudioClip)EditorGUILayout.ObjectField(item.clip, typeof(AudioClip), false, GUILayout.Width(180));
            EditorGUILayout.LabelField("Volume", GUILayout.Width(60));
            item.volume = EditorGUILayout.Slider(item.volume, 0f, 1f);
            EditorGUILayout.LabelField("2D", GUILayout.Width(20));
            item.is2d = EditorGUILayout.Toggle(item.is2d, GUILayout.Width(20));

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(120))) manager.RemoveItem(item);
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }
}
