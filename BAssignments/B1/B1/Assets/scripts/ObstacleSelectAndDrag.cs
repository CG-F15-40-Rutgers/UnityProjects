using UnityEngine;
using System.Collections;

public class ObstacleSelectAndDrag : MonoBehaviour {

	public float speed = 1.0f;

	private Transform objSelected;
	private bool selected;
	private Color startcolor;
	private SelectorScript agentSelector;

	void Start(){
		objSelected = null;
		selected = false;
		agentSelector = GetComponent<SelectorScript> ();

	}

	void FixedUpdate () {
		if (Input.GetButtonDown ("Fire1")) {  
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);     
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 200) && (hit.collider.tag == "obstacle")) {
				if(objSelected==null){
					agentSelector.MassRemove();
					Select(hit.transform);
				}else if(hit.transform!=objSelected){
					Deselect ();
					agentSelector.MassRemove();
					Select(hit.transform);
				}else{
					Deselect();
				}
			}else{
				//Debug.Log(objSelected);
				if(objSelected!=null)
					Deselect ();
			}
		}

		if (selected) {
			Navigate();
		}

	}

	public void Select(Transform obj){
		//Debug.Log ("boop");
		selected = true;
		objSelected = obj;
		startcolor = obj.gameObject.GetComponent<Renderer>().material.color;
		obj.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
	}

	public void Deselect(){
		//Debug.Log (startcolor);
		objSelected.gameObject.GetComponent<Renderer>().material.color = startcolor;
		selected = false;
		objSelected = null;

	}

	public bool isSelected(){
		return selected;
	}

	void Navigate(){
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical"));
		objSelected.position += move * speed * Time.deltaTime;

	}
}