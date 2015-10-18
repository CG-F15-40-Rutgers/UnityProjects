using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public enum Movement{x,z};
	public Movement movement;
	public float distance=4.0f;
	public float speed=1.0f;

	private Vector3 initialPosition;
	private bool positiveDirection;

	void Start () {
		initialPosition = transform.position;
		positiveDirection = true;
	}
	
	void Update () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		
		if(Mathf.Abs(transform.position.x-initialPosition.x)>distance||Mathf.Abs(transform.position.z-initialPosition.z)>distance){
			if(positiveDirection)
				positiveDirection=false;
			else positiveDirection=true;
		}
		Move ();

	}

	void Move(){
		if (movement == Movement.x) {
			if(positiveDirection){
				transform.Translate( new Vector3(1.0f,0,0.0f)*speed*Time.deltaTime,Space.World);
			}else{
				transform.Translate( new Vector3(-1.0f,0,0.0f)*speed*Time.deltaTime,Space.World);
			}
		} else {
			if(positiveDirection){
				transform.Translate( new Vector3(0.0f,0,1.0f)*speed*Time.deltaTime,Space.World);
			}
			else{
				transform.Translate( new Vector3(0.0f,0,-1.0f)*speed*Time.deltaTime,Space.World);
			}
		}
	}
}
