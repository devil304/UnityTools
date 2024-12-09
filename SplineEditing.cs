using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.Splines;

public class SplineEditing
{
    [MenuItem("Tools/MoveSplineKnotToCamera #&%f")] //shortcut ctrl+alt+shift+f
    static void MoveKnotToCamera()
    {
        Debug.Log(Selection.activeObject);
        if (!Selection.activeGameObject.TryGetComponent<ISplineContainer>(out var splineContainer))
            return;
        var splineInfos = new List<SplineInfo>();
        for (int i = 0; i < splineContainer.Splines.Count; i++)
        {
            splineInfos.Add(new SplineInfo(splineContainer, i));
        }
        var selected = SplineSelection.GetActiveElement(splineInfos);
        if (selected != null && selected.GetType() == typeof(SelectableKnot))
        {
            SelectableKnot knot = (SelectableKnot)selected;
            knot.Position = SceneView.lastActiveSceneView.camera.transform.position;
            knot.Rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        }
    }

    [MenuItem("Tools/AddSplineKnotToCamera #&%d")] //shortcut ctrl+alt+shift+d
    static void AddKnotAtCamera()
    {
        Debug.Log(Selection.activeObject);
        if (!Selection.activeGameObject.TryGetComponent<ISplineContainer>(out var splineContainer))
            return;
        var splineInfos = new List<SplineInfo>();
        for (int i = 0; i < splineContainer.Splines.Count; i++)
        {
            splineInfos.Add(new SplineInfo(splineContainer, i));
        }
        var selected = SplineSelection.GetActiveElement(splineInfos);
        if (selected != null && selected.GetType() == typeof(SelectableKnot))
        {
            var index = selected.KnotIndex;
            var selectedInfo = selected.SplineInfo;
            selectedInfo.Container.Splines[selectedInfo.Index].Insert(index + 1, new BezierKnot());
            SelectableKnot knot = new SelectableKnot(selectedInfo, index + 1);
            SplineSelection.SetActive(knot);
            knot.Position = SceneView.lastActiveSceneView.camera.transform.position;
            knot.Rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
        }
    }
}
