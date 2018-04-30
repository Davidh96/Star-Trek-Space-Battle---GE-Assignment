using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public int partsCount=10;
    public GameObject part;

    public float radius = 5.0F;
    public float power = 10.0F;

    // Use this for initialization
    void Start () {

		for(int i=0;i< partsCount; i++)
        {
            //Debug.Log("In loop");
            Instantiate(part, transform.position, Random.rotation);
            Rigidbody rb = part.GetComponent<Rigidbody>();
            rb.AddExplosionForce(power, transform.position, radius, 3.0f);
        }

        Invoke("KillMe", 5);
    }

    void KillMe()
    {
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
