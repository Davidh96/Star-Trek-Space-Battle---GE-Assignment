using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Seek : SteeringBehaviour
{
    public GameObject targetGameObject = null;
    public Vector3 target = Vector3.zero;
    public float stopDistance = 110;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            if (targetGameObject != null)
            {
                target = targetGameObject.transform.position;
            }
            Gizmos.DrawLine(transform.position, target);
        }
    }
    
    public override Vector3 Calculate()
    {
        if (this.GetComponent<Seek>().targetGameObject != null)
        {
            float dist = Vector3.Distance(this.GetComponent<Seek>().targetGameObject.transform.position, transform.position);
            if (dist > stopDistance)
            {
                return boid.SeekForce(target);
            }
        }

            return Vector3.zero;
    }

    public void Update()
    {
        if (targetGameObject != null)
        {
            target = targetGameObject.transform.position;
        }
    }
}