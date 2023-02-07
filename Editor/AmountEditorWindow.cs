using UnityEngine;
using UnityEditor;
using System;

public class AmountEditorWindow : EditorWindow
{
    public event Action<int> OnConfirm;
    public event Action OnClose;
    int amount = 0;
    void OnGUI()
    {
        this.maxSize = new Vector2(150,60);
        amount = EditorGUILayout.IntField(amount);
        if (GUILayout.Button(new GUIContent("ok")))
        {
            OnConfirm?.Invoke(amount);
            this.Close();
        }
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }
    void OnCloseGUI()
    {
        OnClose?.Invoke();
    }
}
