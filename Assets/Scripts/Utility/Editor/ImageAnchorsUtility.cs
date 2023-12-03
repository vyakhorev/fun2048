using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class ImageAnchorsUtility : Editor
{
    [MenuItem("GameObject/RectTransform/Set Anchors %t")]
    private static void SetAnchors()
    {
        RectTransform childRectTransform = Selection.activeTransform as RectTransform;
        if (childRectTransform == null) return;
        RectTransform parentRectTransform = childRectTransform.parent as RectTransform;
        if(parentRectTransform == null) return;
        Vector2 tempPivot = parentRectTransform.pivot;
        parentRectTransform.pivot = new Vector2(0.5f, 0.5f);
        float xMin = (childRectTransform.localPosition.x + parentRectTransform.rect.size.x / 2 - childRectTransform.rect.size.x * childRectTransform.pivot.x) / parentRectTransform.rect.size.x;
        float yMin = (childRectTransform.localPosition.y + parentRectTransform.rect.size.y / 2 - childRectTransform.rect.size.y * childRectTransform.pivot.y) / parentRectTransform.rect.size.y;
        float xMax = (childRectTransform.localPosition.x + parentRectTransform.rect.size.x / 2 + childRectTransform.rect.size.x * (1 - childRectTransform.pivot.x)) / parentRectTransform.rect.size.x;
        float yMax = (childRectTransform.localPosition.y + parentRectTransform.rect.size.y / 2 + childRectTransform.rect.size.y * (1 - childRectTransform.pivot.y)) / parentRectTransform.rect.size.y;
        childRectTransform.anchorMin = new Vector2(xMin, yMin);
        childRectTransform.anchorMax = new Vector2(xMax, yMax);
        childRectTransform.offsetMin = Vector2.zero;
        childRectTransform.offsetMax = Vector2.zero;
        parentRectTransform.pivot = tempPivot;

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }

    [MenuItem("GameObject/RectTransform/Set Anchors %t", true)]
    private static bool CheckRectTransformValidation() => Selection.activeTransform is RectTransform;
}
