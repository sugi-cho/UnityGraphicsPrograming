using UnityEngine;
using System.Collections;

public class Click2Mesh : MonoBehaviour {
	
	Mesh mesh;
	float preX,firstX;
	bool drag;
	// Use this for initialization
	void Start () {
		mesh = ((MeshFilter)FindObjectOfType(typeof(MeshFilter))).mesh = new Mesh();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			preX = Input.mousePosition.x;
			firstX = preX;
			drag = false;
		}
		if(Input.GetMouseButton(0)){
			transform.RotateAround(Vector3.zero, Vector3.up, (Input.mousePosition.x-preX)*Time.deltaTime*10f);
			preX = Input.mousePosition.x;
			drag = preX != firstX;
		}
		if(Input.GetMouseButtonUp(0) && !drag){
			Vector3 pos = Input.mousePosition;
			pos.z = 50f;
			pos = camera.ScreenToWorldPoint(pos);
			
			GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = pos;
			
			Vector3[] newVertices = new Vector3[mesh.vertexCount+1];
			System.Array.Copy(mesh.vertices, newVertices, mesh.vertexCount);
			newVertices[mesh.vertexCount] = pos;
			mesh.vertices = newVertices;
			
			if(mesh.vertexCount % 3 == 0){
				int[] indices = new int[mesh.vertexCount];
				for(int i = 0; i < mesh.vertexCount; i++)
					indices[i] = i;
				mesh.SetIndices(indices, MeshTopology.Triangles, 0);
				mesh.RecalculateNormals();
			}
		}
	}
}
