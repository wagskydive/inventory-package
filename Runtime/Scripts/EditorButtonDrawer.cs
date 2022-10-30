using UnityEngine;
using UnityEditor;
namespace InventoryPackage
{



[CustomPropertyDrawer(typeof(ReadFileAttribute))]

public class EditorButtonDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect labelPosition = new Rect(position.x, position.y, position.width, 16f);
        Rect buttonPosition = new Rect(position.x, position.y + labelPosition.height ,position.width, 16f);

        EditorGUI.LabelField(labelPosition, new GUIContent(property.floatValue.ToString()));
        if(GUI.Button(buttonPosition, "Read File"))
        {
            ReadFileAttribute readFileAttribute = (ReadFileAttribute)attribute;
            
            property.stringValue = InventoryHandler.LibraryNames(JSONDeserializer.CreateLibraryFromJSON(readFileAttribute.filePath)).ToString();
        }
    }


}
}
