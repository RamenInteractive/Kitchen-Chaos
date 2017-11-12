using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Camera pov;
    public static GameObject lHand, rHand;
    public float dropForce = 1.0f;
    private float charge = -0.3f;
    private bool charging = false;

    float initFoV;

    public void dropItem()
    {
        if(rHand != null)
        {
            rHand.transform.parent = GameObject.Find("GameController").transform;
            rHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            rHand.GetComponent<Rigidbody>().detectCollisions = true;
            rHand.GetComponent<Rigidbody>().useGravity = true;
            rHand.transform.position = this.pov.transform.position + this.pov.transform.forward;
            rHand.GetComponent<Rigidbody>().AddForce((pov.transform.forward) * dropForce * (charge + 1));
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
            Debug.Log(rHand);
        }
    }

    private void Start()
    {
        initFoV = pov.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q) && charge < 10f)
        {
            charging = true;
            charge += 2.25f * Time.deltaTime;
            if (charge > 4.5f)
            {
                charge = 4.5f;
            }
            float val = charge < 0 ? 0 : charge;
            pov.fieldOfView = initFoV + (val / (val + 0.35f)) * 13;
        }
        else if (charging)
        {
            charging = false;
            if (charge < 0)
            {
                charge = 0;
            }
            dropItem();
            charge = -0.3f;
            pov.fieldOfView = initFoV;
        }
    }
}
