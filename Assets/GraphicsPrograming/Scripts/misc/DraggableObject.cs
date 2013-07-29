using UnityEngine;
using System.Collections;

public class DraggableObject : MonoBehaviour
{
	bool moving;
	float distance;
	void OnMouseDown ()
	{
		moving = true;
		distance = Vector3.Distance(Camera.mainCamera.transform.position, transform.position);
	}
 
	void OnMouseDrag ()
	{
		if(!moving)
			return;
		Vector3 pos = Input.mousePosition;
		pos.z = distance;
		transform.position = Camera.mainCamera.ScreenToWorldPoint(pos);
	}
	
	void OnMouseUp(){
		moving = false;
	}
}
