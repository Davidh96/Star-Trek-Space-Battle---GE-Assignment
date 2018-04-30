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
        //int cameraFocusShip = Random.Range(0, shipCount);
        int j=0;
        int randomLeader = Random.Range(0, shipCount);
        Vector3 newPos;

        for (int i = 0; i < shipCount; i++)
        {
            //Vector3 newPos = Random.insideUnitCircle;
            ///newPos *= radius;
            Quaternion startRot;
            newPos = transform.position;
            //var euler = transform.eulerAngles;
            //euler.z = Random.rotation.y;
            //transform.eulerAngles = euler;
            
            if (obj.CompareTag("DominionShip"))
            {
                startRot = Quaternion.LookRotation(-transform.forward, Vector3.up);
 
                //targetAngles = obj.transform.eulerAngles + 180f * Vector3.up;
                //obj.transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetAngles, smooth * Time.deltaTime);
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

            //if(obj.transform.position.x<20 & obj.transform.position.x > -20)
            //{
            //    Destroy(obj);
            //}

            var clone = Instantiate(obj, new Vector3(transform.position.x +(20*j), transform.position.y + Random.Range(20, 100),newPos.z ),startRot );

            if (obj.CompareTag("DominionShip"))
            {
                if (randomLeader == i)
                {
                    clone.tag = "DominionLeader";
                }
                
            }
        }

        //if (i % 2 == 0)
        //{
        //    obj.GetComponent<StateMachine>().ChangeState(new HarmonicMovementState(), obj.GetComponent<Boid>());
        //}
        //else if(i== cameraFocusShip)
        //{
        //    obj.GetComponent<Seek>().target = new Vector3(0, 0, 0);
        //    obj.GetComponent<StateMachine>().ChangeState(new SeekTarget(), obj.GetComponent<Boid>());
        //}
        //else
        //{
        //    obj.GetComponent<Seek>().target = obj.transform.TransformPoint((Random.insideUnitSphere*100)+obj.transform.forward);
        //    obj.GetComponent<StateMachine>().ChangeState(new SeekTarget(), obj.GetComponent<Boid>());

        //}

        //Debug.Log("Moving Out!___");
        //obj.GetComponent<Arrive>().targetGameObject = fleetLeader;
        //obj.GetComponent<StateMachine>().ChangeState(new ArriveAtTarget(), obj.GetComponent<Boid>());

    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
