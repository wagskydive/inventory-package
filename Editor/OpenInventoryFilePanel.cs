using System.IO;
using UnityEngine;
using UnityEditor;

public class OpenInventoryFilePanel : EditorWindow
{
    static void Apply()
    {
        string path = EditorUtility.OpenFilePanel("Open Inventory File", "", "inventory");
        Texture2D texture = Selection.activeObject as Texture2D;
        if (texture == null)
        {
            EditorUtility.DisplayDialog("Select Inventory", "You must select a inventory first! You &(%#@$ !! ", "Whatever");
            return;
        }

        
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllBytes(path);
            texture.LoadImage(fileContent);
        }
    }
}