using UnityEngine;
using System.Collections;
using TreeSharpPlus;

public class BehaviorTree : MonoBehaviour {

    public Transform wander1, wander2, wander3, wander4, wander5, wander6, lamp, door;
    public GameObject participant1, participant2, participant3, participant4, participant5;
    private GameObject[] participants;
    private BehaviorAgent behaviorAgent;
    private bool openDoor;

    // Use this for initialization
    void Start () {
        participants = new GameObject[5];
        participants[0] = participant1;
        participants[1] = participant2;
        participants[2] = participant3;
        participants[3] = participant4;
        participants[4] = participant5;

        openDoor = false;

        behaviorAgent = new BehaviorAgent(this.BuildTreeRoot());
        BehaviorManager.Instance.Register(behaviorAgent);
        behaviorAgent.StartBehavior();
	}
	
	// Update is called once per frame
	void Update () {
	    if (openDoor)
        {
            // open door
        }
	}

    protected Node ST_Approach(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return participant.GetComponent<BehaviorMecanim>().Node_GoTo(position);
    }

    protected Node ST_ApproachAndWait(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(participant.GetComponent<BehaviorMecanim>().Node_GoTo(position), new LeafWait(1000));
    }

    protected Node ST_FaceTowards(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return participant.GetComponent<BehaviorMecanim>().Node_OrientTowards(position);
    }

    protected Node ST_OpenDoor()
    {
        openDoor = true;
        return new LeafWait(5000);
    }

    protected Node ST_ActivateLamp(GameObject participant, Transform target)
    {
        Val<Vector3> position = Val.V(() => target.position);
        return new Sequence(
            participant.GetComponent<BehaviorMecanim>().Node_GoToUpToRadius(position, 2),
            participant.GetComponent<BehaviorMecanim>().ST_PlayBodyGesture("PICKUPRIGHT", 2000),
            this.ST_OpenDoor()
        );
    }

    protected Node BuildTreeRoot()
    {
        return
            new Sequence(
                new SequenceParallel(
                    this.ST_Approach(participant1, wander1),
                    this.ST_Approach(participant2, wander2),
                    this.ST_Approach(participant3, wander3),
                    this.ST_Approach(participant4, wander4),
                    this.ST_Approach(participant5, wander5)
                ),
                new SequenceParallel(
                    this.ST_FaceTowards(participant1, wander6),
                    this.ST_FaceTowards(participant2, wander6),
                    this.ST_FaceTowards(participant3, wander6),
                    this.ST_FaceTowards(participant4, wander6),
                    this.ST_FaceTowards(participant5, wander6)
                ),
                new SequenceParallel(
                    participant1.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HANDSUP", 2000),
                    participant2.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HANDSUP", 2000),
                    participant3.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HANDSUP", 2000),
                    participant4.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HANDSUP", 2000),
                    participant5.GetComponent<BehaviorMecanim>().ST_PlayHandGesture("HANDSUP", 2000)
                ),
                participants[(int)Random.Range(0, 4)].GetComponent<BehaviorMecanim>().ST_PlayHandGesture("CROWDPUMP", 2000),
                new SequenceParallel(
                    participant1.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 1000),
                    participant2.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 1000),
                    participant3.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 1000),
                    participant4.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 1000),
                    participant5.GetComponent<BehaviorMecanim>().ST_PlayFaceGesture("ACKNOWLEDGE", 1000)
                ),
                this.ST_ActivateLamp(participants[(int)Random.Range(0, 4)], lamp)
            );
    }
}
