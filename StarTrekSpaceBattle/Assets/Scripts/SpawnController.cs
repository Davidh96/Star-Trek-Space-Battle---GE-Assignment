using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public int shipCount=10;
    public GameObject obj;
    public GameObject fleetLeader;
    public float radius=50;

	// Use this for initialization
	void Start () {
        int cameraFocusShip = Random.Range(0, shipCount);

        for (int i = 0; i < shipCount; i++)
        {
            Vector3 newPos = Random.insideUnitCircle;
            newPos *= radius;
            var euler = transform.eulerAngles;
            euler.z = Random.rotation.y;
            transform.eulerAngles = euler;
            Instantiate(obj, new Vector3(newPos.x,Random.Range(20,200),50), Random.rotation);

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

	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
