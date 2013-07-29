using UnityEngine;
using System.Collections;

public class Drag2Rotate : MonoBehaviour {
	public float rotate = 10f;
	float preMouseX;
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 2 || Input.GetMouseButtonDown(1)){
			Transform[] ts = transform.GetComponentsInChildren<Transform>();
			for(int i = 0; i < ts.Length; i++)
				if(ts[i] != transform)
					Destroy(ts[i].gameObject);
		}
		
		if(Input.GetMouseButtonDown(0))
			preMouseX = Input.mousePosition.x;
		if(!Input.GetMouseButton(0))
			return;
	
		transform.Rotate(Vector3.up, (preMouseX - Input.mousePosition.x)*Time.deltaTime*rotate);
		preMouseX = Input.mousePosition.x;
	}
}
