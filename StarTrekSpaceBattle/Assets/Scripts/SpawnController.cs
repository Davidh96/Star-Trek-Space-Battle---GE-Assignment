using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public int shipCount=10;
    public GameObject obj;
    public GameObject fleetLeader;
    public float radius=50;
    public float smooth = 1f;
    private Vector3 targetAngles;

    // Use this for initialization
    void Start () {
        int j=0;
        int randomLeader = Random.Range(0, shipCount);
        Vector3 newPos;

        for (int i = 0; i < shipCount; i++)
        {

            Quaternion startRot;
            newPos = transform.position;
            
            if (obj.CompareTag("DominionShip"))
            {
                startRot = Quaternion.LookRotation(-transform.forward, Vector3.up);
            }
            else
            {
                startRot = transform.rotation;
            }
            j = i;

            if (j % 2 == 0)
            {
                j *= -1;
            }

            if(i>3 & i < 7)
            {
                newPos.z -= 80;
            }
            else
            {
                newPos.z += 80;
            }


            var clone = Instantiate(obj, new Vector3(transform.position.x +(20*j), transform.position.y + Random.Range(20, 100),newPos.z ),startRot );

            if (obj.CompareTag("DominionShip"))
            {
                if (randomLeader == i)
                {
                    clone.tag = "DominionLeader";
                }
                
            }
        }


    }
}
