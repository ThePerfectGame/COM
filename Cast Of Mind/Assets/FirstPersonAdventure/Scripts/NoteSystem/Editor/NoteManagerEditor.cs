using UnityEngine;
using UnityEditor;

public class NoteManagerEditor : EditorWindow
{
    private NoteManager manager;
    private Color defaultColor;

    [MenuItem("Adventure/Notes")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<NoteManagerEditor>();
    }

    private void OnGUI()
    {
        manager = NoteManager.Instance;
        defaultColor = GUI.backgroundColor;
        if (manager == null) return;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) manager.AddNote();
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(manager.gameObject, PrefabUtility.GetPrefabParent(manager.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(manager.gameObject);
        EditorGUILayout.EndHorizontal();

        foreach (Note note in manager.notes)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            note.idNote= EditorGUILayout.TextField(note.idNote, GUILayout.Width(120));
            EditorGUILayout.LabelField("Name", GUILayout.Width(50));
            note.name = EditorGUILayout.TextField(note.name, GUILayout.Width(120));
            EditorGUILayout.LabelField("Text", GUILayout.Width(50));
            note.text = EditorGUILayout.TextField(note.text);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                manager.RemoveNote(note);
                return;
            }
            GUI.backgroundColor = defaultColor;

            EditorGUILayout.EndHorizontal();
        }
    }
}
