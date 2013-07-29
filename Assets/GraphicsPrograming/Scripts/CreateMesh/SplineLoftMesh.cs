using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplineLoftMesh : MonoBehaviour {
	[HideInInspector()]
	public List<Vector3> points;
	public int
		numSplines = 3,
		numSplinePoints = 3,
		numMeshSections = 8;
	public bool closeSpline;
	
	Mesh mesh;
	
	public Mesh CreateMesh(){
		Vector3[] vertices = new Vector3[numSplines * (numMeshSections+1)];
		Vector2[] uv = new Vector2[numSplines * (numMeshSections+1)];
		
		for(int i = 0; i < numSplines; i++){
			for(int j = 0; j <= numMeshSections; j++){
				vertices[i * (numMeshSections+1) + j] = Spline.GetPointOfPath(GetPath(i) ,(float)j/(float)numMeshSections);
				uv[i * (numMeshSections+1) + j] = new Vector2((float)j/(float)(numMeshSections) ,(float)i/(float)(numSplines - 1));
			}
		}
		int[] indices = new int[numMeshSections*(numSplines-1)*2*3];
		for(int i = 0; i < numSplines-1; i++){
			for(int j = 0; j < numMeshSections; j++){
				indices[i * numMeshSections * 6 + j*6 + 0] = i*(numMeshSections+1) + j;
				indices[i * numMeshSections * 6 + j*6 + 1] = (i+1)*(numMeshSections+1) + j;
				indices[i * numMeshSections * 6 + j*6 + 2] = i*(numMeshSections+1) + (j + 1);
				
				indices[i * numMeshSections * 6 + j*6 + 3] = i*(numMeshSections+1) + (j + 1);
				indices[i * numMeshSections * 6 + j*6 + 4] = (i+1)*(numMeshSections+1) + j;
				indices[i * numMeshSections * 6 + j*6 + 5] = (i+1)*(numMeshSections+1) + (j+1);
			}
		}
		if(mesh == null)
			mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);
		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		return mesh;
	}
	
	public void AddSpline(Spline sp){
		
	}
	
	void OnDrawGizmosSelected(){
		for(int i = 0; i < numSplines; i++)
			Spline.DrawPathHelper(GetPath(i),Color.red);
	}
	
	public Vector3[] GetPath(int n, bool spline = false){
		List<Vector3> v3s = new List<Vector3>();
		for(int i = 0; i < numSplinePoints; i++)
			v3s.Add(points[n * numSplines + i]);
		if(closeSpline && !spline)
			v3s.Add(v3s[0]);
		return v3s.ToArray();
	}
}
