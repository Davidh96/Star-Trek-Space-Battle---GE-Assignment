using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SeekTarget : State
{
    Seek arrive;
    GameObject enemyTarget;
    GameObject[] enemies;
    float attackDistance = 110;

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
        //get array of enemies
        if (boid.gameObject.CompareTag("StarFleet"))
        {
            enemies = GameObject.FindGameObjectsWithTag("DominionShip");
        }

        if (boid.gameObject.CompareTag("DominionShip"))
        {
            enemies = GameObject.FindGameObjectsWithTag("StarFleet");
        }

        enemyTarget = enemies[0];

        //if seeking target
        if (boid.GetComponent<Seek>().targetGameObject != null)
        {
            //if target is close neough, attack
            if (Vector3.Distance(boid.GetComponent<Seek>().targetGameObject.transform.position, boid.transform.position) < attackDistance)
            {
                boid.GetComponent<Attack>().target = boid.GetComponent<Seek>().targetGameObject.GetComponent<Boid>();
                boid.GetComponent<StateMachine>().ChangeState(new AttackState(), boid);
            }
        }

        //get nearby enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemyTarget.transform.position, boid.gameObject.transform.position) > Vector3.Distance(enemies[i].transform.position, boid.gameObject.transform.position) || boid.GetComponent<Seek>().target == null)
            {
                enemyTarget = enemies[i];
            }
        }

        boid.GetComponent<Seek>().targetGameObject = enemyTarget;

    }
}


//state for arriving at a target
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

        //if slowed down
        if (boid.force.magnitude <= 4)
        {
            //if a runabout
            if (boid.gameObject.CompareTag("RunaboutLeader") || boid.gameObject.CompareTag("Runabout"))
            {
                boid.gameObject.tag = "StarFleet";

                //get list of enemies
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("DominionShip");
                //pick random enemy
                int victim = Random.Range(0, enemies.Length);
                //seek enemy
                boid.GetComponent<Seek>().targetGameObject = enemies[victim];
                boid.GetComponent<StateMachine>().ChangeState(new SeekTarget(), boid);
                


            }///if star fleet leader
            if (boid.gameObject.CompareTag("FleetLeader"))
            {
                    //start path following
                    Path path = GameObject.FindGameObjectWithTag("Path").GetComponent<Path>();
                    boid.GetComponent<FollowPath>().path = path;
                    boid.GetComponent<StateMachine>().ChangeState(new FollowPathState(), boid);
            }//if dominion leader,
            if (boid.gameObject.CompareTag("DominionLeader"))
            {
                boid.gameObject.tag = "DominionShip";
                

            }
        }
    }
}

//state for pursuing a target
class PursueTarget : State
{
    Pursue pursue;
    GameObject[] enemies;
    GameObject enemyTarget;
    float attackDistance = 110;

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
        //get array of enemies
        if (boid.gameObject.CompareTag("StarFleet"))
        {
            enemies = GameObject.FindGameObjectsWithTag("DominionShip");
        }

        if (boid.gameObject.CompareTag("DominionShip"))
        {
            enemies = GameObject.FindGameObjectsWithTag("StarFleet");
        }
        enemyTarget = enemies[0];

        //if pursuing target
        if (boid.GetComponent<Pursue>().target != null)
        {
            //if target is in range, attack!
            if (Vector3.Distance(boid.GetComponent<Pursue>().target.transform.position, boid.transform.position) < attackDistance)
            {
                boid.GetComponent<Attack>().target = boid.GetComponent<Pursue>().target;
                boid.GetComponent<StateMachine>().ChangeState(new AttackState(), boid);
            }
        }

        //get nearest enemy
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemyTarget.transform.position, boid.gameObject.transform.position) > Vector3.Distance(enemies[i].transform.position, boid.gameObject.transform.position)  || boid.GetComponent<Pursue>().target==null)
            {
                enemyTarget = enemies[i];
            }
        }

        boid.GetComponent<Pursue>().target = enemyTarget.GetComponent<Boid>();


    }
}

//state for offset pursue
class OffsetPursueTarget : State
{
    OffsetPursue offsetPursue;
    GameObject[] enemies=null;

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

        //get array of enemies
        if (boid.gameObject.CompareTag("StarFleet"))
        {
            enemies = GameObject.FindGameObjectsWithTag("DominionShip");
        }
        else if (boid.gameObject.CompareTag("RunaboutLeader") || boid.gameObject.CompareTag("Runabout"))
        {
            
            boid.gameObject.tag = "StarFleet";

            enemies = GameObject.FindGameObjectsWithTag("DominionShip");

        }
        else if (boid.gameObject.CompareTag("DominionShip") )
        {

            enemies = GameObject.FindGameObjectsWithTag("StarFleet");



        }

        //if enemy nearby, seek!
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(enemies[i].transform.position, boid.gameObject.transform.position)<=boid.GetComponent<Pursue>().shootingDistance || boid.GetComponent<OffsetPursue>().leader==null)
            {

                boid.GetComponent<Seek>().targetGameObject = enemies[i];
                boid.GetComponent<StateMachine>().ChangeState(new SeekTarget(), boid);
            }
        }
    }
}

//state for attacking enemies
class AttackState : State
{
    Attack attack;
    GameObject enemyTarget;
    GameObject[] enemies;

    public override void Enter()
    {
        Debug.Log("Entering Attack Mode!");
        attack = boid.GetComponent<Attack>();
        attack.SetActive(true);
    }

    public override void Exit()
    {
        attack.SetActive(false);
    }

    public override void Think()
    {

        //get arrayy of enemies
        if (boid.gameObject.CompareTag("StarFleet"))
        {
            enemies = GameObject.FindGameObjectsWithTag("DominionShip");
        }
        else if (boid.gameObject.CompareTag("DominionShip"))
        {
            enemies = GameObject.FindGameObjectsWithTag("StarFleet");
        }

        //if object has a current target
        if (boid.GetComponent<Attack>().target == null)
        {
            enemyTarget = enemies[0];

            //find nearest enemy
            for(int i = 1; i < enemies.Length; i++)
            {
                if (Vector3.Distance(enemies[i].transform.position, boid.transform.position) < Vector3.Distance(enemyTarget.transform.position, boid.transform.position))
                {
                    enemyTarget = enemies[i];
                }
            }

            Debug.Log("Acquiring new target");
            //seek new target
            boid.GetComponent<Seek>().targetGameObject = enemyTarget;
            boid.GetComponent<StateMachine>().ChangeState(new SeekTarget(), boid);

        }
    }
}

//state for following a path
class FollowPathState : State
{
    FollowPath followPath;

    public override void Enter()
    {
        Debug.Log("Entering Follow Path Mode!");
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

//state for harmonic movement
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

        //if a leader
        if (this.CompareTag("FleetLeader") || this.CompareTag("DominionLeader"))
        {
            //arrive at a position 300 units ahead
            GetComponent<Arrive>().targetPosition = transform.TransformPoint(0, 0, 350);
            GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), this.gameObject.GetComponent<Boid>());
        }//if runabout leader, arrive at position 400 units ahead
        else if (this.CompareTag("RunaboutLeader"))
        {
            GetComponent<Arrive>().targetPosition = transform.TransformPoint(0, 0, 450);
            GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), this.gameObject.GetComponent<Boid>());

        }//if a runabout(aka. follower)
        else if (this.CompareTag("Runabout"))
        {
            //get nearest leader

            GameObject leader=null;
            //get list of runabout leaders
            GameObject[] leaders = GameObject.FindGameObjectsWithTag("RunaboutLeader");
            if (leaders.Length > 0)
            {
                leader = leaders[0];
            }

            for(int i = 1; i < leaders.Length; i++)
            {
                //if current leader is further away than this new leader, switch leader
                if (Vector3.Distance(leader.transform.position, this.gameObject.transform.position)> Vector3.Distance(leaders[i].transform.position, this.gameObject.transform.position))
                {
                    leader = leaders[i];
                }
            }

            //offset pursue leader
            GetComponent<OffsetPursue>().leader = leader.GetComponent<Boid>();
            GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), this.gameObject.GetComponent<Boid>());

        }
        else//all other ships
        {
            Invoke("MoveOut", 5);
        }
    }

    void MoveOut()
    {
        if (this.CompareTag("StarFleet"))
        {
            //offset pursue fleet leader
            GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("FleetLeader").GetComponent<Boid>();
        }
        if (this.CompareTag("DominionShip"))
        {
            //offset pursue dominion leader
            GetComponent<OffsetPursue>().leader = GameObject.FindGameObjectWithTag("DominionLeader").GetComponent<Boid>();
        }
        GetComponent<StateMachine>().ChangeState(new OffsetPursueTarget(), this.gameObject.GetComponent<Boid>());
    }

}
