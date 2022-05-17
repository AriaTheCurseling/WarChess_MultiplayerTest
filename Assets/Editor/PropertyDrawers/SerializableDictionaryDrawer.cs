using Assets.Editor.Extentions;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
public class SerializableDictionaryDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var dict = property.Get() as ICanHaveDuplicates;
        SerializedProperty entries = property.FindPropertyRelative("entries");
        bool duplicates = dict.HasDuplicates;

        return EditorGUI.GetPropertyHeight(entries, label, true)
             + (duplicates ? 20f : 0f);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var dict = property.Get() as ICanHaveDuplicates;
        SerializedProperty entries = property.FindPropertyRelative("entries");

        EditorGUI.PropertyField(position, entries, label, true);

        if (dict.HasDuplicates)
        {
            float h = EditorGUI.GetPropertyHeight(entries, label, true);
            position.y += h > EditorGUIUtility.singleLineHeight ? h - EditorGUIUtility.singleLineHeight : h;
            position.height = 20;
            position.width -= h > EditorGUIUtility.singleLineHeight ? 110 : 100;
            EditorGUI.HelpBox(position, "Contains duplicate keys", MessageType.Warning);
            position.x += position.width;
            position.width = h > EditorGUIUtility.singleLineHeight ? 40 : 100;

            if (GUI.Button(position, "Fix"))
                dict.RemoveDuplicates();
        }
    }
}

[CustomPropertyDrawer(typeof(SerializableDictionary<,>.Entry))]
public class SerializableDictionary_EntryDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        SerializedProperty key = property.FindPropertyRelative("Key");
        SerializedProperty value = property.FindPropertyRelative("Value");

        return EditorGUI.GetPropertyHeight(key, label, true)
             + EditorGUI.GetPropertyHeight(value, label, true)
             + 15;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty key = property.FindPropertyRelative("Key");
        SerializedProperty value = property.FindPropertyRelative("Value");

        EditorGUI.PropertyField(position, key, new GUIContent(key.displayName), true);
        position.y += EditorGUI.GetPropertyHeight(key, label, true) + 5;
        EditorGUI.PropertyField(position, value, new GUIContent(value.displayName), true);
    }
}
