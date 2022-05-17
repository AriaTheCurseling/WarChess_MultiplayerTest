using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(OptionalAttribute))]
public class OptionalAttributeDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    => EditorGUI.GetPropertyHeight(property, label, true);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject target = property.serializedObject;
        OptionalAttribute optionalAttribute = attribute as OptionalAttribute;

        position.width -= 24;
        EditorGUI.BeginDisabledGroup(optionalAttribute.Enabled);
        EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.EndDisabledGroup();

        position.x += position.width + 24;
        position.width = EditorGUI.GetPropertyHeight(SerializedPropertyType.Boolean, label);
        position.x -= position.width;

        optionalAttribute.Enabled = EditorGUI.Toggle(position, optionalAttribute.Enabled);
    }
}
