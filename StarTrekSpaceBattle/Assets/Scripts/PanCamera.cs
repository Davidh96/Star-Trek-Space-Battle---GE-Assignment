using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour {

    public GameObject target;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(switchTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.transform);
        }
    }

    IEnumerator switchTarget()
    {
        yield return new WaitForSeconds(3);
        while(true){
            GameObject[] dominionObj = GameObject.FindGameObjectsWithTag("DominionShip");
            GameObject[] starfleetObj = GameObject.FindGameObjectsWithTag("StarFleet");

            int side = Random.Range(0, 2);

            if(side ==0)
            {
                target = dominionObj[Random.Range(0,dominionObj.Length)];
            }
            else
            {
                target = starfleetObj[Random.Range(0, starfleetObj.Length)];
            }

            

            yield return new WaitForSeconds(3);
        }
    }
}
