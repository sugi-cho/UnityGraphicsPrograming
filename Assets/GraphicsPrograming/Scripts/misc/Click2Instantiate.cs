using UnityEngine;
using System.Collections;

public class Click2Instantiate : MonoBehaviour {
	public GameObject o;
	
	// Update is called once per frame
	void Update () {
		if(!Input.GetMouseButtonUp(0))
			return;
		
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit)){
			GameObject go = (GameObject)Instantiate(o, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
			go.transform.parent = hit.transform;
		}
	}
}
