using System.IO;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class InventroyEditorWindow : EditorWindow
{
    [SerializeField]
    GameObject inventoryPackageManager;

    [MenuItem("Inventory Package/Inventory Editor")]
    static void ShowWindow()
    {
        GetWindow<InventroyEditorWindow>("Inventory Editor");
    }
        
     void OnGUI() 
    {
        GUILayout.Label("Inventroy Editor");

        //inventoryPackageManager = EditorGUILayout.PropertyField()
        //GUILayout.ObjectField(inventoryPackageManager
        if(GUILayout.Button("Create Library"))
        {
            
        }
    }
    GUILayoutOption[] ObjectOptions()
    {
        GUILayoutOption[] options = new GUILayoutOption[1];
        return options;

    }

}