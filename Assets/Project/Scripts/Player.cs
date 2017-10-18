using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Camera pov;
    public GameObject lHand, rHand;

    public void dropItem()
    {
        if(rHand != null)
        {
            rHand.transform.parent = GameObject.Find("GameController").transform;
            rHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            rHand.GetComponent<Rigidbody>().detectCollisions = true;
            rHand.GetComponent<Rigidbody>().useGravity = true;
            rHand = null;
        }
    }

    public void pickUp(GameObject item)
    {
        if (rHand == null)
        {
            item.transform.parent = GameObject.Find("HandPosition").transform;
            item.transform.localPosition = Vector3.zero;
            this.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            rHand = item;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            dropItem();
        }
    }
}
