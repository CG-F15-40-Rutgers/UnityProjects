using UnityEngine;
using System.Collections.Generic;

public class NazgulScript : MonoBehaviour
{
	private NavMeshAgent agent; 
	private NavMeshObstacle obstacle;
	private Material mat;
	
	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent> ();
		obstacle = GetComponent<NavMeshObstacle> ();

		AgentToObstacle ();

		mat = GetComponent<Renderer>().material;
		mat.SetColor("_Color", Color.magenta);
		
		//agent.SetDestination(new Vector3(20, 1, 20));
	}
	
	// Update is called once per frame
	void Update()
	{
	}
	
	void SetDestination (Vector3 point)
	{
		ObstacleToAgent ();
		agent.SetDestination(point);
	}
	
	public bool AtGoal() {
		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					AgentToObstacle ();
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
			mat.SetColor("_Color", Color.black);
		}
		else
		{
			mat.SetColor("_Color", Color.magenta);
		}
		
	}

	void AgentToObstacle(){
		agent.enabled = false;
		obstacle.enabled = false;
	}

	void ObstacleToAgent(){
		obstacle.enabled = false;
		agent.enabled = true;
	}
	
}