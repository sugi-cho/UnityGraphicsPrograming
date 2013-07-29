using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SplineSweepMesh))]
public class DrawTree : MonoBehaviour {
	public float
		radius = 3f,
		growSpeed = 1f,
		brunchRate = 0.5f,
		speedDamp = 0.95f,
		scaleDamp = 0.99f,
		scaleUp = 1.2f,
		rotateRnd = 0.1f,
		minRadius = 0.1f;
	public int
		numBrunchies = 15,
		numGrow2Brunch = 10;
	public Vector3
		firstVelocity,
		gravity;
	
	[HideInInspector()]
	public bool copy;
	
	SplineSweepMesh sweep;
	
	void Awake(){
		sweep = GetComponent<SplineSweepMesh>();
		sweep.ResetMesh();
	}
	
	// Use this for initialization
	IEnumerator Start () {
		for(int i = 0; i < sweep.path.Length; i++){
			sweep.path[i].x += Random.value/2f;
			sweep.path[i].y += Random.value/2f;
		}
		if(copy)
			transform.rotation *= Quaternion.Lerp(transform.rotation, Random.rotation, rotateRnd);
		Vector3 velocity = firstVelocity;
		sweep.drawPoint = -Vector3.up*(radius/4f);
		sweep.Draw(sweep.drawPoint, true);
		sweep.Draw(sweep.drawPoint + velocity/10000f,false, false, true);
		while(numBrunchies >= 0){
			for(int i = 0; i < numGrow2Brunch; i++){
				yield return 0;
				radius *= 1f - scaleDamp/(float)numGrow2Brunch;
				radius = Mathf.Max(radius, minRadius);
				sweep.scale = radius;
				if(sweep.Draw(sweep.drawPoint + velocity/(float)numGrow2Brunch))
					sweep.UpdateMesh();
				velocity *= 1f - speedDamp/(float)numGrow2Brunch;
				velocity += gravity/(float)numGrow2Brunch;
				velocity = Quaternion.Lerp(Quaternion.identity, Random.rotation, rotateRnd/(float)numGrow2Brunch) * velocity;
			}
			numBrunchies--;
			velocity = Quaternion.Lerp(Quaternion.identity, Random.rotation, rotateRnd) * velocity;
			if(Random.value < brunchRate && numBrunchies > 0){
				GameObject go = (GameObject)Instantiate(gameObject, transform.TransformPoint(sweep.drawPoint), Quaternion.LookRotation(transform.TransformDirection(velocity)));
				go.transform.parent = transform.parent;
				go.transform.localScale = transform.localScale;
			}
			radius *= scaleUp;
			velocity *= scaleUp;
		}
		if(sweep.Draw(sweep.drawPoint + velocity/(float)numGrow2Brunch, false, true))
			sweep.UpdateMesh();
		yield return 0;
	}
}
