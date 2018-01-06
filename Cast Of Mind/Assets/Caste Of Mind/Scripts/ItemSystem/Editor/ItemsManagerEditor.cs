using UnityEngine;
using UnityEditor;

public class ItemsManagerEditor : EditorWindow
{
    private ItemsManager manager;
    private Color defaultColor;

    [MenuItem("Adventure/Items")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ItemsManagerEditor>();
    }

    private void OnGUI()
    {
        manager = ItemsManager.Instance;
        defaultColor = GUI.backgroundColor;
        if (manager == null) return;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) manager.AddNewItem();
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(manager.gameObject, PrefabUtility.GetPrefabParent(manager.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(manager.gameObject);
        EditorGUILayout.EndHorizontal();

        foreach (Item item in manager.items)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            item.idItem = EditorGUILayout.TextField(item.idItem, GUILayout.Width(120));
            EditorGUILayout.LabelField("Use Type", GUILayout.Width(70));
            item.useItemType = (UseItemType)EditorGUILayout.EnumPopup(item.useItemType, GUILayout.Width(120));
            EditorGUILayout.LabelField("Name", GUILayout.Width(50));
            item.name = EditorGUILayout.TextField(item.name, GUILayout.Width(120));
            EditorGUILayout.LabelField("Description", GUILayout.Width(80));
            item.description = EditorGUILayout.TextField(item.description);
            EditorGUILayout.LabelField("Icon", GUILayout.Width(50));
            item.icon = (Sprite)EditorGUILayout.ObjectField(item.icon, typeof(Sprite), false);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                manager.RemoveItem(item);
                return;
            }
            GUI.backgroundColor = defaultColor;

            EditorGUILayout.EndHorizontal();
        }
    }
}
