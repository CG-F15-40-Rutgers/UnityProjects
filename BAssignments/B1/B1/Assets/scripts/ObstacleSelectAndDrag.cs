using UnityEngine;
using System.Collections;

public class ObstacleSelectAndDrag : MonoBehaviour {

	//NEED INFO ABOUT IF OBJECTS ARE CURRENTLY SELECTED FOR AGENTS SO WE CAN INTERCHANGE SELECT/DESELECT
	public float speed = 1.0f;

	private Transform objSelected;
	private bool selected;
	private Color startcolor;

	void Start(){
		objSelected = null;
		selected = false;
	}

	void FixedUpdate () {
		if (Input.GetButtonDown ("Fire1")) {  
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);     
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 200) && (hit.collider.tag == "obstacle")) {
				if(objSelected!=null&&hit.transform!=objSelected)
					Deselect ();
				Select(hit.transform);
			}else{
				Debug.Log(objSelected);
				if(objSelected!=null)
					Deselect ();
			}
		}

		if (selected) {
			Navigate();
		}

	}

	void Select(Transform obj){
		Debug.Log ("boop");
		selected = true;
		objSelected = obj;
		startcolor = obj.gameObject.GetComponent<Renderer>().material.color;
		obj.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
	}

	void Deselect(){
		Debug.Log (startcolor);
		objSelected.gameObject.GetComponent<Renderer>().material.color = startcolor;
		selected = false;
		objSelected = null;

	}

	void Navigate(){
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
		objSelected.position += move * speed * Time.deltaTime;

	}
}