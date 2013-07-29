using UnityEngine;
using System.Collections;

public class LoftMeshController : MonoBehaviour {
	SplineLoftMesh loft;
	GameObject[] pointObjs;
	float preX;
	// Use this for initialization
	void Start () {
		loft = GetComponent<SplineLoftMesh>();
		System.Collections.Generic.List<GameObject> pObjs = new System.Collections.Generic.List<GameObject>();
		for(int i = 0; i < loft.points.Count; i++){
			GameObject pointObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			pointObj.transform.position = loft.points[i];
			pointObj.AddComponent<DraggableObject>();
			pObjs.Add(pointObj);
		}
		pointObjs = pObjs.ToArray();
		GetComponent<MeshFilter>().mesh = loft.CreateMesh();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < pointObjs.Length; i++){
			loft.points[i] = pointObjs[i].transform.position;
			loft.CreateMesh();
		}
	}
	
}
