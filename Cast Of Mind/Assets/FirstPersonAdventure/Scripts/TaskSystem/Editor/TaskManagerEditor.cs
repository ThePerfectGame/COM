using UnityEngine;
using UnityEditor;

public class TaskManagerEditor : EditorWindow
{
    private TaskManager manager;
    private Color defaultColor;

    [MenuItem("Adventure/Tasks")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<TaskManagerEditor>();
    }

    private void OnGUI()
    {
        manager = TaskManager.Instance;
        defaultColor = GUI.backgroundColor;
        if (manager == null) return;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) manager.AddTask();
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(manager.gameObject, PrefabUtility.GetPrefabParent(manager.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(manager.gameObject);
        EditorGUILayout.EndHorizontal();

        foreach (Task task in manager.tasks)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            task.idTask = EditorGUILayout.TextField(task.idTask, GUILayout.Width(120));
            EditorGUILayout.LabelField("Is Primary", GUILayout.Width(70));
            task.isPrimary = EditorGUILayout.Toggle(task.isPrimary, GUILayout.Width(20));
            EditorGUILayout.LabelField("Name", GUILayout.Width(40));
            task.name = EditorGUILayout.TextField(task.name);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                manager.RemoveTask(task);
                return;
            }
            GUI.backgroundColor = defaultColor;

            EditorGUILayout.EndHorizontal();
        }
    }
}
