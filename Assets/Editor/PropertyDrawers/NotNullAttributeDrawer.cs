using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NotNullAttribute))]
public class NotNullAttributeDrawer : PropertyDrawer
{

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.objectReferenceValue is null)
            return EditorGUI.GetPropertyHeight(property, label, true) + 20;
        else
            return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        position.y += EditorGUI.GetPropertyHeight(property, label, true);
        position.height = 20;

        if (property.objectReferenceValue is null)
        {
            string errorMessage = string.Format(" {0} may not be null", property.displayName);

            EditorGUI.HelpBox(position, errorMessage, MessageType.Error);
            Debug.LogError(errorMessage, property.serializedObject.targetObject);
        }


        //position.x += position.width + 24;
        //position.width = EditorGUI.GetPropertyHeight(SerializedPropertyType.Boolean, label);
        //position.x -= position.width;
    }
}
