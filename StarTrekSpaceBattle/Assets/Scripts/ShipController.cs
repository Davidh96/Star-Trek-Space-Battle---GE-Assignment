using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PursueEnemy : State
{
    //Boid boid;
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

    }
}

public class ShipController : MonoBehaviour {

    public void Restart()
    {
        GetComponent<StateMachine>().ChangeState(new PursueEnemy(),gameObject.GetComponent<Boid>());
    }

    // Use this for initialization
    void Start () {
        Restart();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
