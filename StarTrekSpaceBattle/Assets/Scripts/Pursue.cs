using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class Pursue : SteeringBehaviour
{
    public Boid target;
    Vector3 targetPos;
    public GameObject bulletSpawner;

    public GameObject bulletPrefab;
    bool allowFire = true;
    public int fireRate = 1;
    public float shootingDistance = 300;

    public void Start()
    {

    }

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled && Application.isPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, targetPos);
        }

    }

    public override Vector3 Calculate()
    {
        if (target != null)
        {
            float dist = Vector3.Distance(target.transform.position, transform.position);
            float time = dist / boid.maxSpeed;

            targetPos = target.transform.position
                + (time * target.velocity);

            Vector3 directionToTarget = target.transform.position - transform.position;
            float angle = Vector3.Angle(transform.forward, directionToTarget);
            if (Mathf.Abs(angle) < 25 & dist< shootingDistance)
            {
                if (allowFire)
                {
                    StartCoroutine(Fire());
                }
                //Debug.Log("target is in front of me");
            }

            return boid.SeekForce(targetPos);
        }
        return new Vector3();
    }


    IEnumerator Fire()
    {


        allowFire = false;
        //fire bullets 
        for (int i = 0; i < fireRate; i++)
        {
            Instantiate(bulletPrefab, bulletSpawner.transform.position, transform.rotation);
            yield return new WaitForSeconds(1 / fireRate);
        }

        yield return new WaitForSeconds(1);
        allowFire = true;
    }
}
