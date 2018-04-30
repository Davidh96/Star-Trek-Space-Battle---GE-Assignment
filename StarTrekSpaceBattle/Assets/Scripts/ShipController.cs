using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SeekTarget : State
{
    Seek arrive;

    public override void Enter()
    {
        Debug.Log("Entering Seek Mode!");
        arrive = boid.GetComponent<Seek>();
        arrive.SetActive(true);
    }

    public override void Exit()
    {
        arrive.SetActive(false);
    }

    public override void Think()
    {

    }
}

class ArriveAtTarget : State
{
    Arrive arrive;

    public override void Enter()
    {
        Debug.Log("Entering Arrive Mode!");
        arrive = boid.GetComponent<Arrive>();
        arrive.SetActive(true);
    }

    public override void Exit()
    {
        arrive.SetActive(false);
    }

    public override void Think()
    {
        //Debug.Log("Thinking!");
        if (Vector3.Distance(arrive.targetGameObject.transform.position, boid.transform.position) < Random.Range(30,75))
        {
            boid.GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("FleetLeader").GetComponent<Boid>();
            boid.GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), boid);
        }
    }
}

class PursueTarget : State
{
    Pursue pursue;

    public override void Enter()
    {
        Debug.Log("Entering Pursue Mode!");
        pursue = boid.GetComponent<Pursue>();
        pursue.SetActive(true);
    }

    public override void Exit()
    {
        pursue.SetActive(false);
    }

    public override void Think()
    {
        //if certain distance and target is in sight (dot product) = attackstate
    }
}

class OffsetPursueTarget : State
{
    OffsetPursue offsetPursue;

    public override void Enter()
    {
        Debug.Log("Entering Offset Pursue Mode!");
        offsetPursue = boid.GetComponent<OffsetPursue>();
        offsetPursue.SetActive(true);
    }

    public override void Exit()
    {
        offsetPursue.SetActive(false);
    }

    public override void Think()
    {

    }
}

class FollowPathState : State
{
    FollowPath followPath;

    public override void Enter()
    {
        Debug.Log("Entering Offset Pursue Mode!");
        followPath = boid.GetComponent<FollowPath>();
        followPath.SetActive(true);
    }

    public override void Exit()
    {
        followPath.SetActive(false);
    }

    public override void Think()
    {

    }
}

class HarmonicMovementState : State
{
    HarmonicMovement movement;

    public override void Enter()
    {
        Debug.Log("Entering Harmonic Movement Mode!");
        movement = boid.GetComponent<HarmonicMovement>();
        movement.SetActive(true);
    }

    public override void Exit()
    {
        movement.SetActive(false);
    }

    public override void Think()
    {

    }
}

public class ShipController : MonoBehaviour {

    

    // Use this for initialization
    void Start () {
        if (this.CompareTag("FleetLeader") || this.CompareTag("DominionLeader"))
        {
            ////GetComponent<Seek>().target = 
            //Vector3 ArrivePos = new Vector3(this.forward.x, Vector3.forward.y, Vector3.forward.z * 200);
            //Debug.Log(transform.TransformPoint(ArrivePos));
            GetComponent<Arrive>().targetPosition = transform.TransformPoint(0,0,300);
            GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), this.gameObject.GetComponent<Boid>());
        }
        else
        {
            //GetComponent<StateMachine>().ChangeState(new HarmonicMovementState(), this.gameObject.GetComponent<Boid>());
            Invoke("MoveOut", 10);
        }
    }

    void MoveOut()
    {
        if (this.CompareTag("StarFleet"))
        {
            GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("FleetLeader").GetComponent<Boid>();
        }
        if (this.CompareTag("Dominion"))
        {
            GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("DominionLeader").GetComponent<Boid>();
        }
        GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), this.gameObject.GetComponent<Boid>());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
