﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
    List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();

    public int health = 5;
    
    public Vector3 force = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 velocity = Vector3.zero;
    public float mass = 1;
    public float damping = 0.01f;
    public float maxSpeed = 5.0f;
    public float maxForce = 10.0f;
    public GameObject explosion;



    // Use this for initialization
    void Start () {

        //get all steering behaviours attached to the game object
        SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();

        foreach (SteeringBehaviour b in behaviours)
        {
            this.behaviours.Add(b);
        }
	}


    //seek force steering behaviour
    public Vector3 SeekForce(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        return desired - velocity;
 
    }

    //arrive force steering behaviour
    public Vector3 ArriveForce(Vector3 target, float slowingDistance = 15.0f, float deceleration = 1.0f)
    {
        //get vector to target
        Vector3 toTarget = target - transform.position;

        //calculate distance
        float distance = toTarget.magnitude;
        if (distance < 50)
        {
            return Vector3.zero;
        }

        float ramped = maxSpeed * (distance / (slowingDistance * deceleration));

        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = clamped * (toTarget / distance);

        return desired - velocity;
    }

    private bool AccumulateForce(ref Vector3 runningTotal, ref Vector3 force)
    {
        float soFar = runningTotal.magnitude;
        float remaining = maxForce - soFar;
        Vector3 clampedforce = Vector3.ClampMagnitude(force, remaining);        
        runningTotal += clampedforce;
        return (force.magnitude >= remaining);
    }
    
    //calculate the force being added to the object
    Vector3 Calculate()
    {
        force = Vector3.zero;
        
        foreach (SteeringBehaviour b in behaviours)
        {
            //only get active steering behaviours
            if (b.isActiveAndEnabled)
            {
                Vector3 behaviourForce = b.Calculate() * b.weight;
                bool full = AccumulateForce(ref force, ref behaviourForce);
                if (full)
                {
                    break;
                }
            }
        }
        
        return force;
    }

	
	// Update is called once per frame
	void Update () {
        force = Calculate();
        Vector3 newAcceleration = force / mass;

        float smoothRate = Mathf.Clamp(9.0f * Time.deltaTime, 0.15f, 0.4f) / 2.0f;
        acceleration = Vector3.Lerp(acceleration, newAcceleration, Time.deltaTime);

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //used for banking
        Vector3 globalUp = new Vector3(0, 0.2f, 0);
        Vector3 accelUp = acceleration * 0.05f;
        Vector3 bankUp = accelUp + globalUp;        
        Vector3 tempUp = transform.up;
        tempUp = Vector3.Lerp(tempUp, bankUp, Time.deltaTime * 3);

        if (velocity.magnitude  > 0.0001f)
        {
            transform.LookAt(transform.position + velocity, tempUp);
            velocity *= (1.0f - (damping * Time.deltaTime));
        }

        transform.position += velocity * Time.deltaTime;        
	}


    private void OnTriggerEnter(Collider other)
    {
        //if hit by bullet
        if (other.CompareTag("Bullet"))
        {
            StartCoroutine(takeDamage());
        }
    }

    //take away health form object
    private IEnumerator takeDamage()
    {
        health--;
        if (health == 0)
        {
            //switch camera if object is destroyed
            GameObject camCon = GameObject.FindGameObjectWithTag("CameraController");
            camCon.GetComponent<CameraContoller>().ForceUpdate();
            //create explosion
            Instantiate(explosion, transform.position, transform.rotation);
            //destroy object
            Destroy(this.gameObject);
        }
        //wait 2 seconds before registering damage again
        yield return new WaitForSeconds(2);
    }
}
