using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    public StateMachine owner;
    public Boid boid;
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Think() { }

}

public class StateMachine : MonoBehaviour {

    public State currentState;
    public State previousState;

    private IEnumerator coroutine;

    public int updatesPerSecond = 5;

    private void OnEnable()
    {
        StartCoroutine(Think());
    }

    public void ChangeStateDelayed(State newState, float delay)
    {
        //coroutine = ChangeStateCoRoutine(newState, delay);
        StartCoroutine(coroutine);
    }

    public void CancelDelayedStateChange()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    //IEnumerator ChangeStateCoRoutine(State newState, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    ChangeState(newState);
    //}

    //public void RevertToPreviousState()
    //{
    //    ChangeState(previousState);
    //}

    public void ChangeState(State newState, Boid boid)
    {
        previousState = currentState;
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.owner = this;
        currentState.boid = boid;
        currentState.Enter();
    }

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
