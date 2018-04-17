using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
        Invoke("KillMe", 5);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    void KillMe()
    {
        GameObject.Destroy(this.gameObject);
    }
}
