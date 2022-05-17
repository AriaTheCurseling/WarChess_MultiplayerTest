using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(T<,>))]
public class TDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var Item1 = property.FindPropertyRelative("Item1");
        var Item2 = property.FindPropertyRelative("Item2");
        //var Item3 = property.FindPropertyRelative("Item3");
        //var Item4 = property.FindPropertyRelative("Item4");
        //var Item5 = property.FindPropertyRelative("Item5");
        //var Item6 = property.FindPropertyRelative("Item6");
        //var Item7 = property.FindPropertyRelative("Item7");
        //var Item8 = property.FindPropertyRelative("Item8");

        position.y -= EditorGUIUtility.singleLineHeight;
        EditorGUI.LabelField(position, label);

        position.y += EditorGUIUtility.singleLineHeight;
        position.x += EditorGUIUtility.labelWidth;
        position.width -= EditorGUIUtility.labelWidth + 5;
        position.width /= 2;
        EditorGUI.PropertyField(position, Item1, GUIContent.none, true);
        position.x += position.width + 5;
        EditorGUI.PropertyField(position, Item2, GUIContent.none, true);
    }
}
