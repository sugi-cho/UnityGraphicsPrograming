using UnityEngine;
using System.Collections;

public class ShowMousePos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		guiText.text = Input.mousePosition.x + ", " + Input.mousePosition.y;
	}
}
