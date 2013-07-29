using UnityEngine;
using System.Collections;

public class DrawMesh : MonoBehaviour {
	SplineSweepMesh sweep;
	// Use this for initialization
	void Awake () {
		sweep = GetComponent<SplineSweepMesh>();
		sweep.ResetMesh();
	}
	
	//Update() for sample
	void Update(){
		if(Input.touchCount > 2 || Input.GetMouseButtonDown(1)){
			Destroy(sweep.mesh);
			sweep.ResetMesh();
			GetComponent<MeshFilter>().mesh = sweep.mesh;
		}
		if(!Input.GetMouseButtonDown(0) && !Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0)){
			Camera.mainCamera.transform.RotateAround(sweep.drawPoint, Vector3.up, Time.deltaTime * 10f);
			return;
		}
		
		Vector3 pos = Input.mousePosition;
		pos.z = 50f-Mathf.Sin(Time.time)*20f;
		pos = Camera.mainCamera.ScreenToWorldPoint(pos);
		sweep.scale = Mathf.Sqrt(1f/(sweep.drawPoint - pos).magnitude);
		if(sweep.Draw(pos, Input.GetMouseButtonDown(0), Input.GetMouseButtonUp(0)))
			sweep.UpdateMesh();
	}
}
