using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CreateMesh : MonoBehaviour {
	
	[MenuItem("Assets/Create/Quad")]
	public static void CreateQuad(){
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[4]{
			new Vector3(-0.5f,-0.5f),
			new Vector3(0.5f,-0.5f),
			new Vector3(0.5f,0.5f),
			new Vector3(-0.5f,0.5f)
		};
		Vector3[] normals = new Vector3[4]{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		Vector2[] uv = new Vector2[4]{
			new Vector2(0,0),
			new Vector2(1f,0),
			new Vector2(1f,1f),
			new Vector2(0,1f)
		};
		int[] indices = new int[6]{
			0,2,1,
			0,3,2
		};
		
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uv;
		mesh.SetIndices(indices, MeshTopology.Triangles, 0);
		
		string path = GetCurrentPath() + "Quad.asset";
		AssetDatabase.CreateAsset(mesh, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		
		Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
	}
	
	[MenuItem("Assets/Create/Line Cube")]
	public static void CreateLineCube(){
		Mesh mesh = new Mesh();
		
		Vector3[] vertices = new Vector3[8]{
			new Vector3(-0.5f,-0.5f,-0.5f),
			new Vector3(0.5f,-0.5f,-0.5f),
			new Vector3(0.5f,-0.5f,0.5f),
			new Vector3(-0.5f,-0.5f,0.5f),
			
			new Vector3(-0.5f,0.5f,-0.5f),
			new Vector3(0.5f,0.5f,-0.5f),
			new Vector3(0.5f,0.5f,0.5f),
			new Vector3(-0.5f,0.5f,0.5f)
		};
		Vector2[] uv = new Vector2[8]{
			new Vector2(0,0),
			new Vector2(1f,0),
			new Vector2(1f,1f),
			new Vector2(0,1f),
			
			new Vector2(0,1f),
			new Vector2(1f,1f),
			new Vector2(1f,0),
			new Vector2(0,0)
		};
		int[] indices = new int[24]{
			0,1,
			1,2,
			2,3,
			3,0,
			
			0,4,
			1,5,
			2,6,
			3,7,
			
			4,5,
			5,6,
			6,7,
			7,4
		};
		
		mesh.vertices = vertices;
		mesh.normals = vertices;
		mesh.uv = uv;
		mesh.SetIndices(indices, MeshTopology.Lines, 0);
		
		string path = GetCurrentPath() +"LineCube.asset";
		AssetDatabase.CreateAsset(mesh, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		
		Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
	}
	
	[MenuItem("Assets/Create/Line Circle")]
	public static void CreateLineCircle(){
		Mesh mesh = new Mesh();
		Vector3[] vertices = new Vector3[33];
		Vector2[] uv = new Vector2[33];
		int[] indices = new int[33];
		
		for(int i = 0; i < 33; i++){
			float fi = (float)i;
			vertices[i] = new Vector3(Mathf.Sin(fi/32f * 2f * Mathf.PI), Mathf.Cos(fi/32f * 2f * Mathf.PI), 0) * 0.5f;
			uv[i] = new Vector2(fi/32f, 0);
			indices[i] = i;
		}
		
		mesh.vertices = vertices;
		mesh.normals = vertices;
		mesh.uv = uv;
		mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
		
		string path = GetCurrentPath() + "LineCircle.asset";
		AssetDatabase.CreateAsset(mesh, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		
		Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
	}
	
	[MenuItem("Assets/Create/Line Mesh from Mesh")]
	public static void CreateLineMesh(){
		Mesh original = (Mesh)Selection.activeObject;
		if(original == null){
			Debug.Log("please select mesh!");
			return;
		}
		
		string name = original.name;
		Mesh mesh = (Mesh)Instantiate(original);
		int[] indices = mesh.GetIndices(0);
		List<Line> lines = new List<Line>();
		
		for(int i = 0; i < indices.Length/3; i++){
			int first,second;
			
			first = indices[i*3];
			second = indices[i*3+1];
			if(!CheckLineAlreadyExists(lines.ToArray(), first, second)){
				lines.Add(new Line(first, second));
			}
			
			first = indices[i*3+1];
			second = indices[i*3+2];
			if(!CheckLineAlreadyExists(lines.ToArray(), first, second)){
				lines.Add(new Line(first, second));
			}
			
			first = indices[i*3+2];
			second = indices[i*3];
			if(!CheckLineAlreadyExists(lines.ToArray(), first, second)){
				lines.Add(new Line(first, second));
			}
		}
		
		List<int> newIndices = new List<int>();
		foreach(Line line in lines){
			newIndices.Add(line.first);
			newIndices.Add(line.second);
		}
		
		mesh.SetIndices(newIndices.ToArray(), MeshTopology.Lines, 0);
		string path = GetCurrentPath() + name + "_Line.asset";
		AssetDatabase.CreateAsset(mesh, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		
		Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(Mesh));
	}
	
	[MenuItem("Assets/Create/points65000")]
	public static void CreatePointMesh(){
		Mesh m = new Mesh();
		m.MarkDynamic();
		
		Vector3[] vertices = new Vector3[65000];
		Vector3[] normals = new Vector3[65000];
		Vector2[] uv = new Vector2[65000];
		Color[] colors = new Color[65000];
		int[] indices = new int[65000];
		
		for(int i = 0; i < 65000; i++){
			vertices[i] = Random.insideUnitSphere;
			normals[i] = new Vector3(Random.value, Random.value, Random.value);
			colors[i] = new Color(Random.value, Random.value, Random.value);
			uv[i] = new Vector2(Random.value, Random.value);
			indices[i] = i;
		}
		
		m.vertices = vertices;
		m.normals = normals;
		m.uv = uv;
		m.colors = colors;
		m.SetIndices(indices, MeshTopology.Points, 0);
		
		string path = GetCurrentPath() + "Points.asset";
		AssetDatabase.CreateAsset(m, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	[MenuItem("Assets/Create/PointTexture")]
	public static void CreatePointTexture(){
		Texture2D tex = (Texture2D)Selection.activeObject;
		if(tex == null) return;
		
		Texture2D dispMap = new Texture2D(512, 512);
		float longSide = (float)Mathf.Max(tex.width, tex.height);
		
		Color[] pixels = new Color[512*512];
		int index = 0;
		for(int x = 0; x < tex.width; x++){
			for(int y = 0; y < tex.height; y++){
				if(tex.GetPixel(x,y).a > 0.5f){
					float
						posX = ((float)x)/longSide,
						posY = ((float)y)/longSide;
					
					Color c = Color.white;
					c.r = (posX % 0.1f) * 10f;
					c.g = posX - c.r/10f;
					c.b = (posY % 0.1f) * 10f;
					c.a = posY - c.b/10f;
					
					pixels[index++] = c;
				}
			}
		}
		
		for(int i = index; i < 512*512; i++){
			pixels[i] = pixels[i%index];
		}
		
		dispMap.SetPixels(pixels);
		dispMap.filterMode = FilterMode.Point;
		
		string path = GetCurrentPath() + tex.name + "_Disp.asset";
		AssetDatabase.CreateAsset(dispMap, path);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
	[MenuItem("Assets/Create/Mesh Combined")]
	public static void CreateMeshCombined(){
		GameObject[] gos = Selection.gameObjects;
		if(gos.Length == 0)
			return;
		List<CombineInstance> combines = new List<CombineInstance>();
		foreach(GameObject go in gos){
			MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter>();
			foreach(MeshFilter mf in mfs){
				CombineInstance combine = new CombineInstance();
				combine.mesh = mf.mesh;

				combine.transform = mf.transform.localToWorldMatrix;
				combines.Add(combine);
			}
		}
		Mesh mesh = new Mesh();
		mesh.CombineMeshes(combines.ToArray());
		
		AssetDatabase.CreateAsset(mesh, "Assets/CombineMesh.asset");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		
		Selection.activeObject = mesh;
	}
	
	static string GetCurrentPath(){
		string path = "Assets";
		foreach(Object obj in Selection.GetFiltered(typeof(Object),SelectionMode.Assets)){
			path = AssetDatabase.GetAssetPath(obj);
			if(File.Exists(path))
				path = Path.GetDirectoryName(path);
			break;
		}
		return path+"/";
	}
	
	static bool CheckLineAlreadyExists(Line[] lines, int first, int second){
		bool b = false;
		foreach(Line l in lines){
			b &= l.Check(second, first);
		}
		return b;
	}
	
	struct Line{
		public int first,second;
		public Line(int f, int s){
			first = f;
			second = s;
		}
		public bool Check(int f, int s){
			return first == f && second == s;
		}
	}
}
