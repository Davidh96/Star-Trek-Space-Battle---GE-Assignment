using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject enemy;
    bool allowFire = true;
    public int fireRate = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (enemy != null)
        {
            Vector3 directionToTarget = enemy.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (Mathf.Abs(angle) < 25)
            {
                if (allowFire)
                {
                    StartCoroutine(Fire());
                }
                //Debug.Log("target is in front of me");
            }
        }

    }

    IEnumerator Fire()
    {


        allowFire = false;
        //fire bullets 
        for (int i = 0; i < fireRate; i++)
        {
            Instantiate(bulletPrefab, transform.position,bulletPrefab.transform.rotation);
            yield return new WaitForSeconds(1 / fireRate);
        }

        yield return new WaitForSeconds(1);
        allowFire = true;
    }
}
