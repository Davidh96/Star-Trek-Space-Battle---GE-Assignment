using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormSpwanController : MonoBehaviour {

    public float gap = 20;
    public int followers = 2;
    public GameObject prefab;
    int camEnable;

    // Use this for initialization
    void Start () {
        //create leader
        GameObject leader = GameObject.Instantiate<GameObject>(prefab);
        leader.transform.parent = this.transform;
        leader.transform.position = this.transform.position;
        leader.transform.rotation = this.transform.rotation;
        leader.tag = "RunaboutLeader";

        //pick a random follower to enable the camera on
        camEnable = Random.Range(0, followers);

        //create number of followers
        for (int i = 1; i <= followers; i++)
        {
            Vector3 offset = new Vector3(gap * i, 0, -gap * i);
            GameObject follower = CreateFollower(offset, leader.GetComponent<Boid>(),i);
            offset = new Vector3(-gap * i, 0, -gap * i);
            follower = CreateFollower(offset, leader.GetComponent<Boid>(),i);
        }
    }

    GameObject CreateFollower(Vector3 offset, Boid leader,int i)
    {
        //instantiate follower
        GameObject follower = GameObject.Instantiate<GameObject>(prefab);
        follower.transform.position = this.transform.TransformPoint(offset);
        follower.transform.parent = this.transform;
        follower.transform.rotation = this.transform.rotation;

        //disbale camera if not chosen to use camera
        if (i != camEnable)
        {
            if (follower.GetComponentInChildren<Camera>() != null)
            {
                follower.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            }
            
        }

        return follower;
    }
}
