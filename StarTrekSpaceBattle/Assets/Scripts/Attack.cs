using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject bulletPrefab;
    bool allowFire = true;
    public int fireRate = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (allowFire)
        {
            StartCoroutine(Fire());
        }       
        
	}

    IEnumerator Fire()
    {
        allowFire = false;
        //fire bullets 
        for(int i = 0; i < fireRate; i++)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(1/fireRate);
        }

        yield return new WaitForSeconds(1);
        allowFire = true;
    }
}
