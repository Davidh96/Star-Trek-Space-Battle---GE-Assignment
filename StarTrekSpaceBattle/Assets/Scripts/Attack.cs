using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : SteeringBehaviour {

    public Boid target;
    Vector3 targetPos;
    public GameObject bulletSpawner;

    public GameObject bulletPrefab;
    bool allowFire = true;
    public int fireRate = 1;
    public float shootingDistance = 300;
    public float shootingAngle = 25;
    public float stopDistance = 100;

    public override Vector3 Calculate()
    {
        if (target != null)
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.transform.position), Time.deltaTime*10);

            float dist = Vector3.Distance(target.transform.position, transform.position);

            Vector3 directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (Mathf.Abs(angle) < shootingAngle & dist < shootingDistance)
            {
                if (allowFire)
                {
                    StartCoroutine(Fire());
                }
                //Debug.Log("target is in front of me");
            }
        }

        return Vector3.zero;
    }

    // Use this for initialization
    void Start () {
		
	}

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), Time.deltaTime);
    }


    IEnumerator Fire()
    {
        //this.transform.LookAt(target.transform);

        allowFire = false;
        //fire bullets 
        for (int i = 0; i < fireRate; i++)
        {
            Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation);
            bulletPrefab.GetComponent<Seek>().targetGameObject = target.gameObject;
            bulletPrefab.transform.LookAt(target.transform);
            yield return new WaitForSeconds(1 / fireRate);
        }

        yield return new WaitForSeconds(1);
        allowFire = true;
    }
}
