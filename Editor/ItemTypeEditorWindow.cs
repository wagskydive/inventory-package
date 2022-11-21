using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InventoryPackage;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class ItemTypeEditorWindow : EditorWindow
{
    ItemType itemType;

    Texture2D Icon;


    void OnGUI()
    {
        if(itemType == null)
        {
            return;
        }
        GUILayout.Label("Item Type Editor: "+ itemType.TypeName);
        GUILayout.Space(10);

        GUILayout.Label("Name: "+ itemType.TypeName);
        ItemType.SetTypeName(itemType, EditorGUILayout.TextField(itemType.TypeName));
        GUILayout.Space(20);

        GUILayout.Label("Description:");
        ItemType.SetDescription(itemType,GUILayout.TextField(itemType.Description));
        GUILayout.Space(20);
        GUILayout.Label("Icon: ");
        ItemType.SetIcon(itemType,(Texture2D)EditorGUILayout.ObjectField(itemType.Icon, typeof(Texture2D), false,GUILayout.Width(150),GUILayout.Height(150)));
        
        
        if(GUILayout.Button("save", GUILayout.MaxWidth(50)))
        {
            string savePath = "";
            savePath = EditorUtility.SaveFilePanel("Save Texture",savePath, itemType.TypeName, "png");
            if(savePath != "")
            {
                TextureSaver.SaveTextureToFile(itemType.Icon,savePath);
            }
            
        }
        
        //EditorGUILayout.sp
        	

    }

    public void SetItemType(ItemType itemType)
    {
        this.itemType = itemType;
    }
}
