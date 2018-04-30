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
        //if (Vector3.Distance(arrive.targetGameObject.transform.position, boid.transform.position) < Random.Range(30,75))
        //{
        //    boid.GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("FleetLeader").GetComponent<Boid>();
        //    boid.GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), boid);
        //}

        //Debug.Log(boid.force.magnitude);
        if (boid.force.magnitude <= 1)
        {
            //Debug.Log("Her4e");
            if (boid.gameObject.CompareTag("RunaboutLeader"))
            {
                boid.gameObject.tag = "Runabout";

                GameObject[] enemies = GameObject.FindGameObjectsWithTag("DominionShip");
                Debug.Log("Number :" + enemies.Length);
                int victim = Random.Range(0, enemies.Length);


                boid.GetComponent<Pursue>().target = enemies[victim].GetComponent<Boid>();
                boid.GetComponent<StateMachine>().ChangeState(new PursueTarget(), boid);
                


            }
            if (boid.gameObject.CompareTag("DominionLeader"))
            {
                //boid.gameObject.tag = "Dominion";

            }
        }
    }
}

class PursueTarget : State
{
    Pursue pursue;
    GameObject[] enemies;
    GameObject enemyTarget;

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
        enemies = GameObject.FindGameObjectsWithTag("DominionShip");
        enemyTarget = enemies[0];

        //if certain distance and target is in sight (dot product) = attackstate
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemyTarget.transform.position, boid.gameObject.transform.position) > Vector3.Distance(enemies[i].transform.position, boid.gameObject.transform.position))
            {
                enemyTarget = enemies[i];
            }
        }

        boid.GetComponent<Pursue>().target = enemyTarget.GetComponent<Boid>();


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
            GetComponent<Arrive>().targetPosition = transform.TransformPoint(0, 0, 300);
            GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), this.gameObject.GetComponent<Boid>());
        }
        else if (this.CompareTag("RunaboutLeader"))
        {
            GetComponent<Arrive>().targetPosition = transform.TransformPoint(0, 0, 400);
            GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), this.gameObject.GetComponent<Boid>());

            //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Dominion");
        }
        else if (this.CompareTag("Runabout"))
        {
            GameObject[] leaders = GameObject.FindGameObjectsWithTag("RunaboutLeader");
            GameObject leader=leaders[0];

            for(int i = 1; i < leaders.Length; i++)
            {
                if (Vector3.Distance(leader.transform.position, this.gameObject.transform.position)> Vector3.Distance(leaders[i].transform.position, this.gameObject.transform.position))
                {
                    leader = leaders[i];
                }
            }

            GetComponent<OffsetPursue>().leader = leader.GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), this.gameObject.GetComponent<Boid>());

        }
        else if (!this.CompareTag("Runabout"))
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
        if (this.CompareTag("DominionShip"))
        {
            GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("DominionLeader").GetComponent<Boid>();
        }
        GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), this.gameObject.GetComponent<Boid>());
    }

    // Update is called once per frame
    void Update () {
		
	}
}
