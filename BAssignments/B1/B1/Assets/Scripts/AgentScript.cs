using UnityEngine;
using System.Collections.Generic;

public class AgentScript : MonoBehaviour
{
	public float NazgulDistance=5.0f;

	private NavMeshAgent agent;
	private Material mat;
	private Vector3 savedGoal;
	private bool travelAwayFromNazgul;
	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		mat = GetComponent<Renderer>().material;
		mat.SetColor("_Color", Color.blue);
		travelAwayFromNazgul = false;
		savedGoal = new Vector3(0.0f,-100.0f,0.0f);

		//agent.SetDestination(new Vector3(20, 1, 20));
	}
	
	// Update is called once per frame
	void Update()
	{
	
		GameObject[] nazguls = GameObject.FindGameObjectsWithTag ("Nazgul");
		float distance;
		if (!travelAwayFromNazgul) {
			foreach (GameObject nazgul in nazguls) {
				distance = Vector3.Distance (nazgul.transform.position, transform.position);
				if (distance <= NazgulDistance) {
					float x;
					float z;
					if (transform.position.x < nazgul.transform.position.x)
						x = transform.position.x - NazgulDistance;
					else
						x = transform.position.x + NazgulDistance;
					if (transform.position.z < nazgul.transform.position.z)
						z = transform.position.z - NazgulDistance;
					else
						z = transform.position.z + NazgulDistance;
					if (agent.hasPath)
						savedGoal = agent.destination;
					SetDestination (new Vector3 (x, 0.0f, z));
					travelAwayFromNazgul = true;
					break;
				}
			}
		} else {
			if(AtGoal()){
				travelAwayFromNazgul=false;
				if(savedGoal.y!=-100){
					SetDestination(savedGoal);
					savedGoal=new Vector3(0.0f,-100.0f,0.0f);
				}
			}

		}

	}
	
	void SetDestination (Vector3 point)
	{
		agent.SetDestination(point);
	}
	
	public bool AtGoal() {
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					return true;
				}
			}
		}
		return false;
	}
	
	void Selected(bool on)
	{
		if (on)
		{
			mat.SetColor("_Color", Color.green);
		}
		else
		{
			mat.SetColor("_Color", Color.blue);
		}
		
	}
	
}