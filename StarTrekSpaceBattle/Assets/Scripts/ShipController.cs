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
        if (offsetPursue != null)
        {
            Debug.Log("Nope111!");
        }
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
        Debug.Log("Entering Offset Pursue Mode!");
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
        //GetComponent<StateMachine>().ChangeState(new HarmonicMovementState(),this.gameObject.GetComponent<Boid>());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
