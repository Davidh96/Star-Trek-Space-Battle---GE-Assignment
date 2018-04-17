using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public int next =0;

    private void OnDrawGizmos()
    {
        int count = transform.childCount;

        for(int i = 0; i < count; i++)
        {
            Vector3 currentNode = transform.GetChild(i%count).position;
            Vector3 nextNode = transform.GetChild((i + 1)%count).position;
            Gizmos.DrawSphere(currentNode, 1);
            Gizmos.DrawLine(currentNode, nextNode);
        }
    }

    public Vector3 NextNode()
    {
        return transform.GetChild(next).position;
    }

    public Vector3 AdvanceToNext()
    {
        next = (next + 1) % transform.childCount;

        return transform.GetChild(next).position;
    }
}
