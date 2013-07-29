using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class SplineSweepMesh : MonoBehaviour {
	public Vector3[] path;
	public Vector3 drawPoint;
	public int
		sections = 12;
	public float
		scale = 1f,
		minDistance = 0.1f,
		uvDelta = 0.1f;
	public bool closePath;
	
	[HideInInspector()]
	public Mesh mesh;
	
	Vector3[] points;
	bool started;
	float uvY;
	
	Quaternion rot;
	
	Vector3[] meshVertices;
	Vector2[] meshUv;
	int[] meshIndices;
	
	public void CalculatePoints(){
		List<Vector3> ps = new List<Vector3>(path);
		if(closePath)
			ps.Add(ps[0]);
		points = new Vector3[sections+1];
		for(int i = 0; i <= sections; i++){
			float t = (float)i/(float)sections;
			points[i] = Spline.GetPointOfPath(ps.ToArray(), t);
		}
	}
	
	public bool Draw(Vector3 point, bool drawStart = false, bool drawEnd = false, bool draw = false){
		if(drawStart){
			started = true;
			drawPoint = point;
			return false;
		}
		float d = (point - drawPoint).magnitude;
		if(d < minDistance && !drawEnd && !started && ! draw)
			return false;
		int numVerts = meshVertices.Length;
		int[] currentIndices = meshIndices;
		int numIndices = meshIndices.Length;
		
		Vector3[] vertices = new Vector3[numVerts + points.Length * (started? 2:1) + (started? 1:0) + (drawEnd? 1:0)];
		System.Array.Copy(meshVertices, vertices, numVerts);
		
		Vector2[] uv = new Vector2[vertices.Length];
		System.Array.Copy(meshUv, uv, numVerts);
		
		int[] indices = new int[numIndices + sections*6  + (started? sections*3:0) + (drawEnd? sections*3:0)];
		System.Array.Copy(currentIndices, indices, numIndices);
		
		Vector3[] newPoints = GetNewPoints(point);
		
		if(started){
			vertices[numVerts] = drawPoint;
			uvY = 0;
			uv[numVerts] = new Vector2(0.5f, uvY);
			uvY += uvDelta;
			for(int i = 0; i < points.Length; i++){
				vertices[numVerts + i + 1] = newPoints[i]-(point - drawPoint);
				float x = (float)i/(float)(points.Length-1);
				uv[numVerts + i + 1] = new Vector2(x, uvY);
			}
			uvY += uvDelta;
			for(int i = 0; i < points.Length; i++){
				vertices[numVerts + i + points.Length + 1] = newPoints[i];
				float x = (float)i/(float)(points.Length-1);
				uv[numVerts + i + points.Length + 1] = new Vector2(x, uvY);
			}
		}
		else{
			uvY += uvDelta;
			for(int i = 0; i < points.Length; i++){
				vertices[numVerts + i] = newPoints[i];
				float x = (float)i/(float)(points.Length-1);
				uv[numVerts + i] = new Vector2(x, uvY);
			}
			uvY += uvDelta;
			if(drawEnd){
				vertices[numVerts + points.Length] = point;
				uv[numVerts + points.Length] = new Vector2(0.5f, uvY);
			}
		}
		int indicesFrom = vertices.Length - (sections+1)*2 - (drawEnd?1:0);
		int startDelta = started? 3*sections:0;
		if(started){
			for(int i = 0; i < sections; i++){
				indices[numIndices + i*3] = indicesFrom - 1;
				indices[numIndices + i*3 + 1] = indicesFrom + i;
				indices[numIndices + i*3 + 2] = indicesFrom + i + 1;
			}
		}
		for(int i = 0; i < sections; i++){
			indices[startDelta + numIndices + i*6] = indicesFrom + i;
			indices[startDelta + numIndices + i*6 + 1] = indicesFrom + (sections+1) + i;
			indices[startDelta + numIndices + i*6 + 2] = indicesFrom + (i+1);
			
			indices[startDelta + numIndices + i*6 + 3] = indicesFrom + (i+1);
			indices[startDelta + numIndices + i*6 + 4] = indicesFrom + (sections+1) + i;
			indices[startDelta + numIndices + i*6 + 5] = indicesFrom + (sections+1) + (i+1);
		}
		if(drawEnd){
			for(int i = 0; i < sections; i++){
				indices[startDelta + sections * 6 + i*3] = indicesFrom + (sections + 1) + i;
				indices[startDelta + sections * 6 + i*3 + 1] = indicesFrom + (sections +1) * 2;
				indices[startDelta + sections * 6 + i*3 + 2] = indicesFrom + (sections + 1) + (i+1);
			}
		}
		
		meshVertices = vertices;
		meshUv = uv;
		meshIndices = indices;
		drawPoint = point;
		started = false;
		
		return true;
	}
	
	public void UpdateMesh(){
		mesh.vertices = meshVertices;
		mesh.uv = meshUv;
		mesh.SetIndices(meshIndices, MeshTopology.Triangles, 0);
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}
	
	public void ResetMesh(){
		CalculatePoints();
		mesh = new Mesh();
		mesh.MarkDynamic();
		GetComponent<MeshFilter>().mesh  = mesh;
		
		meshVertices = new Vector3[0];
		meshUv = new Vector2[0];
		meshIndices = new int[0];
	}
	
	Vector3[] GetNewPoints(Vector3 point){
		Quaternion rotation = Quaternion.LookRotation(point - drawPoint);
		rot = Quaternion.Lerp(rot, rotation, started? 1f:0.5f);
		Vector3[] newPoints = new Vector3[points.Length];
		for(int i = 0; i < points.Length; i++){
			newPoints[i] = rot * points[i] * scale;
			newPoints[i] += point;
		}
		return newPoints;
	}
	
	void OnDrawGizmosSelected(){
		if(path.Length < 2)
			return;
		Vector3[] v3s = new Vector3[path.Length + (closePath? 1:0)];
		System.Array.Copy(path, v3s, path.Length);
		if(closePath)
			v3s[path.Length] = path[0];
		Spline.DrawPathHelper(v3s, Color.red);
	}
	
	void OnDestroy(){
		if(mesh != null)
			Destroy(mesh);
	}
}
