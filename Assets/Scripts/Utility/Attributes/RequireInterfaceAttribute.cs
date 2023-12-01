using System;
using UnityEngine;

namespace Utility.Attributes
{
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        public Type Type { get; private set; }

        public RequireInterfaceAttribute(Type type)
        {
            Type = type;
        }
    }

    #region Property Drawer
#if UNITY_EDITOR
    [UnityEditor.CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceAttributeDrawer : UnityEditor.PropertyDrawer
    {
        public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
        {
            if(property.propertyType == UnityEditor.SerializedPropertyType.ObjectReference)
            {
                RequireInterfaceAttribute attribute = this.attribute as RequireInterfaceAttribute;

                if (property.isArray)
                {
                    for (int i = 0; i < property.arraySize; i++)
                    {
                        var indexedProperty = property.GetArrayElementAtIndex(i);
                        if (indexedProperty.objectReferenceValue.GetType() != attribute.Type)
                            indexedProperty.objectReferenceValue = null;
                    }
                }
                else
                {
                    
                    UnityEditor.EditorGUI.BeginProperty(position, label, property);
                    property.objectReferenceValue = UnityEditor.EditorGUI.ObjectField(position, label, property.objectReferenceValue, attribute.Type, true);
                    UnityEditor.EditorGUI.EndProperty();
                }
            }
            else
            {
                GUI.color = Color.red;
                UnityEditor.EditorGUI.LabelField(position, label, new GUIContent("Propety type is not Object Reference"));
                GUI.color = Color.white;
            }
            
        }
    }
#endif
#endregion
}