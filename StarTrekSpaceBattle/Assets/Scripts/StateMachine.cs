using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//contains the methods and variables for a generic state
public abstract class State
{
    public StateMachine owner;
    public Boid boid;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Think() { }

}

//controls the switching of states
public class StateMachine : MonoBehaviour {

    public State currentState;

    private IEnumerator coroutine;

    public int updatesPerSecond = 5;

    private void OnEnable()
    {
        StartCoroutine(Think());
    }

    //change current state
    public void ChangeState(State newState, Boid boid)
    {
        //if in a non null state
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.owner = this;
        currentState.boid = boid;
        currentState.Enter();

       
    }

    //check to see if any changes need to be made
    System.Collections.IEnumerator Think()
    {
        
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        while (true)
        {
            if (currentState != null)
            {
                currentState.Think();
            }
            yield return new WaitForSeconds(1.0f / (float)updatesPerSecond);
        }
    }
}
