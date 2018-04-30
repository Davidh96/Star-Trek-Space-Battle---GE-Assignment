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
    public float shootingAngle = 25;
    public float stopDistance = 100;


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
            if (dist > stopDistance)
            {
                float time = dist / boid.maxSpeed;

                targetPos = target.transform.position
                    + (time * target.velocity);

                return boid.SeekForce(targetPos);
            }
            
        }
        return Vector3.zero;
    }


    IEnumerator Fire()
    {
        this.transform.LookAt(target.transform);

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
