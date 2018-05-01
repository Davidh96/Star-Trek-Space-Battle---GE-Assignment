using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //if within 20 units of node
        if (Vector3.Distance(transform.position, target) < 20)
        {
            //go to next node
            target = path.AdvanceToNext();
            Debug.Log("Next node: " +path.next);
            //if on way to last node
            if (path.next % 2 == 0)
            {
                Debug.Log("On Last Stretch");
                //increase speed and force
                this.gameObject.GetComponent<Boid>().maxSpeed = 500;
                this.gameObject.GetComponent<Boid>().maxForce = 500;
                //switch to final camera
                GameObject camCon = GameObject.FindGameObjectWithTag("CameraController");
                camCon.GetComponent<CameraContoller>().ForceSwitch(this.gameObject);

                Invoke("loadEndScene",3);
            }
        }

        return boid.SeekForce(target);

    }

    private void loadEndScene()
    {
        //load credit screen
        SceneManager.LoadScene("EndScene");
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
