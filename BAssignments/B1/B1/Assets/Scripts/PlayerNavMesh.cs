using UnityEngine;
using System.Collections;

public class PlayerNavMesh : MonoBehaviour {
	/*
	Animator animator;
	
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	//	float h = Input.GetAxis("Horizontal");
	//	float v = Input.GetAxis("Vertical");
	//	bool sprint = Input.GetButton("Fire3");
	//	bool jump = Input.GetButtonDown("Jump");
		
		animator.SetFloat("Forward", v);
		animator.SetFloat("Turn", h);
		animator.SetBool("Sprint", sprint);
		animator.SetBool("Jump", jump);
	}*/

	public float NazgulDistance=5.0f;

	public ParticleSystem particles;
	private Animator animator;
	private NavMeshAgent agent;
	private Material mat;
	private Vector3 savedGoal;
	private bool travelAwayFromNazgul;
	private Vector3 prevPosition;
	private CapsuleCollider capsule;
	private Transform Robot;
	private int mode;

	// Use this for initialization
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		capsule = GetComponent<CapsuleCollider>();
		mat = GetComponent<Renderer>().material;
		animator = GetComponentInChildren<Animator>();
		particles = GetComponent<ParticleSystem> ();
		particles.enableEmission = false;
		
		foreach (Transform child in transform) {
			if(child.gameObject.tag=="Robot Kyle"){
				Robot=child;
				break;
			}
			
		}

		mat.SetColor("_Color", Color.blue);
		travelAwayFromNazgul = false;
		savedGoal = new Vector3(0.0f,-100.0f,0.0f);
		prevPosition = transform.position;
		//agent.SetDestination(new Vector3(20, 1, 20));
		mode = 1;
	}
	
	// Update is called once per frame
	void Update()
	{
		Robot.position = transform.position;

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
		if (agent.hasPath) {
			float h = agent.nextPosition.z-prevPosition.z;
			if(h>1) h=1;
			if(h<-1) h=1;
			float v = agent.nextPosition.x-prevPosition.x;
			if (v>1) v=1;
			if(v<-1) v=-1;
			bool sprint = agent.speed==14.0f;
			bool jump = agent.velocity.y > 1;
		
			animator.SetFloat ("Forward", v*agent.speed*5);
			animator.SetFloat ("Turn", h*agent.speed*5);
			animator.SetBool ("Sprint", sprint);
			animator.SetBool ("Jump", jump);
		} else {
			animator.SetFloat ("Forward", 0);
			animator.SetFloat ("Turn", 0);
		}
		prevPosition = agent.nextPosition;

		if (Input.GetKeyDown ("o")) {
			agent.speed=3.5f;
		}
		if (Input.GetKeyDown ("p")) {
			agent.speed=14.0f;
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
			particles.enableEmission=true;
		}
		else
		{
			mat.SetColor("_Color", Color.blue);
			particles.enableEmission=false;
		}
		
	}
}
