using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SplineLoftMesh))]
public class SplineLoftMeshEditor : Editor
{
	public void OnSceneGUI ()
	{
		SplineLoftMesh slm = (SplineLoftMesh)target;
		
		if(slm.points == null)
			return;
		
		for (int i = 0; i < slm.numSplines; i++){
			Vector3[] path = slm.GetPath(i, true);
			for (int j = 0; j < path.Length; j++) {
				Vector3 v3 = Handles.PositionHandle (slm.transform.TransformPoint (path [j]), slm.transform.rotation);
				v3 = slm.transform.InverseTransformPoint (v3);
				if (v3 != path [j]) {
					Undo.RegisterUndo(slm, "path mod");
					slm.points [slm.numSplinePoints*i+j] = v3;
					slm.CreateMesh();
					EditorUtility.SetDirty(target);
				}
			}
		}
	}
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeInspector ();
		DrawDefaultInspector ();
		SplineLoftMesh slm = (SplineLoftMesh)target;
		
		if(slm.points == null)
			slm.points = new List<Vector3>();
		if(slm.numSplines*slm.numSplinePoints > slm.points.Count)
			slm.points.Add(new Vector3());
		if(slm.numSplines*slm.numSplinePoints < slm.points.Count)
			slm.points.RemoveAt(slm.points.Count-1);
		
		for(int i = 0; i < slm.numSplines; i++){
			EditorGUILayout.LabelField("Spline" + i.ToString("000"));
			EditorGUI.indentLevel ++;
			Vector3[] path = slm.GetPath(i, true);
//			if(path.Length != slm.numPoints)
//				path = new Vector3[slm.numPoints];
			for(int j = 0; j < path.Length; j++){
				slm.points[i*slm.numSplinePoints+j] = EditorGUILayout.Vector3Field("point"+j.ToString("000"), path[j]);
			}
			EditorGUI.indentLevel --;
		}
		
		if (GUILayout.Button ("Create Mesh")) {
			Mesh mesh = slm.CreateMesh ();
			slm.GetComponent<MeshFilter> ().mesh = mesh;
		}
	}
}
