using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : SteeringBehaviour {

    public Path path;
    private Vector3 target;

    // Use this for initialization
    void Start()
    {
        target = path.NextNode();
    }

    // Update is called once per frame
    public override Vector3 Calculate()
    {
        if (Vector3.Distance(transform.position, target) < 3)
        {
            target = path.AdvanceToNext();
        }

        return boid.SeekForce(target);

    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            Vector3 nextNode = path.NextNode();
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, nextNode);
        }
    }
}
