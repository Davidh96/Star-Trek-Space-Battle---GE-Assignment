using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosivePartController : MonoBehaviour {

    Vector3 targetScale;

	// Use this for initialization
	void Start () {
        targetScale = new Vector3(0.1f, 0.1f, 0.1f);
        //destroy after 5 seconds
        Invoke("KillMe", 5);
    }

    void KillMe()
    {
        GameObject.Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
        //get smaller over time
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime);
    }
}
