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

        //instantiate parts that will explode form the destroyed object
		for(int i=0;i< partsCount; i++)
        {
            Instantiate(part, transform.position, Random.rotation);
            Rigidbody rb = part.GetComponent<Rigidbody>();
            //add explosive force to the objects
            rb.AddExplosionForce(power, transform.position, radius, 3.0f);
        }
        //destroy explosive parts after 5 seconds
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
