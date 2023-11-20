using System.Collections;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;

namespace Utility.DataStructures.Editor
{
    [CustomPropertyDrawer(typeof(SerializableHashMap<,>), true)]
    public class SerializableHashMapPropertyDrawer : PropertyDrawer
    {
        private const string KEYS = "_keys";
        private const string VALUES = "_values";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Object targetObject = property.serializedObject.targetObject;
            ICollection map = fieldInfo.GetValue(targetObject) as ICollection;
            return EditorGUIUtility.singleLineHeight * map.Count + EditorGUIUtility.standardVerticalSpacing * map.Count + EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty keysProperty = property.FindPropertyRelative(KEYS);
            SerializedProperty valuesProperty = property.FindPropertyRelative(VALUES);

            EditorGUI.BeginProperty(position, GUIContent.none, property);

            Rect line = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            float oneFifth = position.width / 5;
            float twoFifth = oneFifth * 2f;

            GUIContent languageContent = new("Key:");
            GUIContent txtContent = new("Value:");
            float languageContentWidth = GUI.skin.label.CalcSize(languageContent).x;
            float txtContentWidth = GUI.skin.label.CalcSize(txtContent).x;

            for (int i = 0; i < keysProperty.arraySize; i++)
            {
                SerializedProperty iKeyProperty = keysProperty.GetArrayElementAtIndex(i);
                SerializedProperty iValueProperty = valuesProperty.GetArrayElementAtIndex(i);
                EditorGUI.PrefixLabel(new Rect(line.x, line.y, twoFifth, line.height), languageContent, EditorStyles.label);
                EditorGUI.PropertyField(new Rect(line.x + languageContentWidth, line.y, twoFifth - languageContentWidth, line.height), iKeyProperty, GUIContent.none);
                EditorGUI.PrefixLabel(new Rect(line.x + twoFifth, line.y, twoFifth, line.y), txtContent);
                EditorGUI.PropertyField(new Rect(line.x + twoFifth + txtContentWidth, line.y, twoFifth - txtContentWidth, line.height), iValueProperty, GUIContent.none);
                DrawRemoveButton(new Rect(line.x + twoFifth * 2, line.y, oneFifth, line.height), keysProperty, valuesProperty, i);
                line = new Rect(line.x, line.y + EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight, line.width, line.height);
            }

            DrawAddButton(new Rect(line.x + twoFifth, line.y, oneFifth, line.height), keysProperty, valuesProperty);
            EditorGUI.EndProperty();
        }

        private void DrawAddButton(Rect rect, SerializedProperty keysProperty, SerializedProperty valuesProperty)
        {
            bool isPressed = GUI.Button(rect, "+", EditorStyles.miniButtonMid);

            if (!isPressed)
                return;

            keysProperty.arraySize++;
            SetDefaultValue(keysProperty.GetArrayElementAtIndex(keysProperty.arraySize - 1));
            valuesProperty.arraySize++;
        }

        private void DrawRemoveButton(Rect rect, SerializedProperty keysProperty, SerializedProperty valuesProperty, int index)
        {
            bool isPressed = GUI.Button(rect, "-", EditorStyles.miniButtonMid);

            if (!isPressed)
                return;

            keysProperty.DeleteArrayElementAtIndex(index);
            valuesProperty.DeleteArrayElementAtIndex(index);
        }

        private void SetDefaultValue(SerializedProperty targetKeyProperty)
        {
            switch (targetKeyProperty.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    targetKeyProperty.intValue = default;
                    break;
                case SerializedPropertyType.Boolean:
                    targetKeyProperty.boolValue = default;
                    break;
                case SerializedPropertyType.Float:
                    targetKeyProperty.floatValue = default;
                    break;
                case SerializedPropertyType.String:
                    targetKeyProperty.stringValue = default;
                    break;
                case SerializedPropertyType.Color:
                    targetKeyProperty.colorValue = default;
                    break;
                case SerializedPropertyType.ObjectReference:
                    targetKeyProperty.objectReferenceValue = default;
                    break;
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    Type type = Type.GetType(targetKeyProperty.type);
                    string[] values = Enum.GetNames(type);
                    targetKeyProperty.enumValueIndex = (int)Enum.Parse(type, values[0]);
                    break;
                case SerializedPropertyType.Vector2:
                    targetKeyProperty.vector2Value = default;
                    break;
                case SerializedPropertyType.Vector3:
                    targetKeyProperty.vector3Value = default;
                    break;
                case SerializedPropertyType.Vector4:
                    targetKeyProperty.vector4Value = default;
                    break;
                case SerializedPropertyType.Rect:
                    targetKeyProperty.rectValue = default;
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    targetKeyProperty.stringValue = default;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    targetKeyProperty.animationCurveValue = default;
                    break;
                case SerializedPropertyType.Bounds:
                    targetKeyProperty.boundsValue = default;
                    break;
                case SerializedPropertyType.Gradient:
                    targetKeyProperty.gradientValue = default;
                    break;
                case SerializedPropertyType.Quaternion:
                    targetKeyProperty.quaternionValue = default;
                    break;
                case SerializedPropertyType.ExposedReference:
                    targetKeyProperty.exposedReferenceValue = default;
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    targetKeyProperty.vector2IntValue = default;
                    break;
                case SerializedPropertyType.Vector3Int:
                    targetKeyProperty.vector3IntValue = default;
                    break;
                case SerializedPropertyType.RectInt:
                    targetKeyProperty.rectIntValue = default;
                    break;
                case SerializedPropertyType.BoundsInt:
                    targetKeyProperty.boundsIntValue = default;
                    break;
                case SerializedPropertyType.ManagedReference:
                    targetKeyProperty.managedReferenceValue = default;
                    break;
                case SerializedPropertyType.Hash128:
                    targetKeyProperty.hash128Value = default;
                    break;
            }
        }
    }
}