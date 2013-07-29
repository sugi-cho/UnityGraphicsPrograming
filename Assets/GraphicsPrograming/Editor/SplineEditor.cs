using UnityEngine;
using UnityEditor;
using System.Collections;

public class SplineEditor {
	public static void SplineEditHelper(Vector3[] path, Transform transform){
		Vector3[] points = path;
		for(int i = 0; i < points.Length; i++){
			Vector3 v3 = Handles.PositionHandle(transform.TransformPoint(points[i]), transform.rotation);
			v3 = transform.InverseTransformPoint(v3);
			if(v3 != points[i]){
				points[i] = v3;
			}
		}
	}
}
