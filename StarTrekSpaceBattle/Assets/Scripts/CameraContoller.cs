using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContoller : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //disable all cameras except for one random one

        //get all cameras
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        //get random int, this will be the starting camera
        int RandomStartCame = Random.Range(0, cameras.Length);

        for (int i = 0; i < cameras.Length; i++)
        {
            //diable camera component
            cameras[i].GetComponent<Camera>().enabled = false;
            cameras[i].GetComponent<AudioListener>().enabled = false;

            //enable selected camera
            if (i == RandomStartCame)
            {
                cameras[i].GetComponent<Camera>().enabled = true;
                cameras[i].GetComponent<AudioListener>().enabled = true;
            }
        }

        StartCoroutine(switchCamera());
    }

    //switch camera after 5 seconds
    IEnumerator switchCamera()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
          
            ForceUpdate();
            yield return new WaitForSeconds(5);

        }

    }

    //forces a camera change
    public void ForceUpdate()
    {
        //get updated list of cameras
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        //pick a random camera
        int cameraSwitchTo = Random.Range(0, cameras.Length);
        Debug.Log("Camera Count: " + cameras.Length);

        //diable all cameras except the selected one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = false;
            cameras[i].GetComponent<AudioListener>().enabled = false;
            if (i == cameraSwitchTo)
            {
                cameras[i].GetComponent<Camera>().enabled = true;
                cameras[i].GetComponent<AudioListener>().enabled = true;
            }
        }
    }

    //called to switch to the final camera
    public void ForceSwitch(GameObject obj)
    {
        //get final camera object
        GameObject finCam = GameObject.FindGameObjectWithTag("FinalCamera");
        finCam.GetComponent<Camera>().enabled = true;

        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");

        Debug.Log("Camera Count: " + cameras.Length);
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].GetComponent<Camera>().enabled = false;
            cameras[i].GetComponent<AudioListener>().enabled = false;

        }
    }
}
