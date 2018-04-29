using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class SteeringBehaviour:MonoBehaviour
{
    public float weight = 1.0f;
    public Vector3 force;

    [HideInInspector]
    public Boid boid;

    [HideInInspector]
    public bool active = true;

    public void Awake()
    {
        boid = GetComponent<Boid>();
    }


    public void SetActive(bool b)
    {
        Debug.Log("Here!");
        active = b;
        enabled = b;
        Debug.Log("Here2!");
    }

    public abstract Vector3 Calculate();
}
