using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingGame : MonoBehaviour {

    public GameObject guiObject;
    public Camera player;
    public Camera cuttingGame;
    private bool switchCam = false;
    private bool backCam = false;

	// Use this for initialization
	void Start () {
        guiObject.SetActive(false);
        player.GetComponent<Camera>().enabled = true;
        cuttingGame.GetComponent<Camera>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            guiObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        guiObject.SetActive(false);
        if (guiObject.activeInHierarchy == true && Input.GetMouseButtonDown(0))
        {
            switchCam = !switchCam;

            if (switchCam == true)
            {
                player.GetComponent<Camera>().enabled = false;
                cuttingGame.GetComponent<Camera>().enabled = true;
            }

            if (switchCam == false)
            {
                player.GetComponent<Camera>().enabled = true;
                cuttingGame.GetComponent<Camera>().enabled = false;
            }
        }

    }
}
