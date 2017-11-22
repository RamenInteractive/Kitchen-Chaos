using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Camera pov;
    public GameObject lHand, rHand;
    public float dropForce = 1.0f;
    private float charge = -0.1f;
    private bool charging = false;
    private bool canLThrow = true;
    private bool canRThrow = true;
    private Controller controller;
    private float lhCharge = -0.1f;
    private bool lhCharging = false;

    float initFoV;

    public void dropItem(bool leftHand)
    {
        GameObject hand = leftHand ? lHand : rHand;
        if(hand != null)
        {
            hand.transform.parent = GameObject.Find("GameController").transform;
            hand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            hand.GetComponent<Rigidbody>().detectCollisions = true;
            hand.GetComponent<Rigidbody>().useGravity = true;
            hand.transform.position = this.pov.transform.position + this.pov.transform.forward;
            hand.GetComponent<Rigidbody>().AddForce((pov.transform.forward) * dropForce * ((leftHand ? lhCharge : charge) + 1));
            if (leftHand) {
                lHand = null;
            }
            else {
                rHand = null;
            }
        }
    }

    public void pickUp(GameObject item, bool leftHand)
    {
        if ((leftHand ? lHand : rHand) == null)
        {
            item.transform.parent = leftHand ? GameObject.Find("LeftHandPosition").transform
                    : GameObject.Find("HandPosition").transform;
            item.transform.localPosition = Vector3.zero;
            this.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().detectCollisions = false;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (leftHand) {
                lHand = item;
                canLThrow = false;
            }
            else {
                rHand = item;
                canRThrow = false;
            }
        }
    }

    private void Start()
    {
        initFoV = pov.fieldOfView;
        controller = GetComponent<Controller>();
    }

    private void Update()
    {
        // controller.GetButton("LeftHand") && charge < 10f && lHand != null
        float tempFoV = initFoV;
        if (charging && !controller.GetButtonUp("RightHand") && charge < 4.5f)
        {
            charge += 2.25f * Time.deltaTime;
            if (charge > 4.5f)
            {
                charge = 4.5f;
            }
            float val = charge - 0.4f < 0 ? 0 : charge - 0.4f;

            float tmp = initFoV + (val / (val + 0.35f)) * 13.4f;
            if (tmp > tempFoV) {
                tempFoV = tmp;
            }
        }
        else if (charging)
        {
            charging = false;
            if (charge < 0)
            {
                charge = 0;
            }
            dropItem(false);
            charge = -0.1f;
        }
        if (lhCharging && !controller.GetButtonUp("LeftHand") && lhCharge < 4.5f) {
            lhCharge += 4.5f * Time.deltaTime;
            if (lhCharge > 4.5f) {
                lhCharge = 4.5f;
            }
            float val = lhCharge - 0.4f < 0 ? 0 : lhCharge - 0.4f;

            float tmp = initFoV + (val / (val + 0.35f)) * 13.4f;
            if (tmp > tempFoV) {
                tempFoV = tmp;
            }
        }
        else if (lhCharging) {
            lhCharging = false;
            if (lhCharge < 0) {
                lhCharge = 0;
            }
            dropItem(true);
            lhCharge = -0.1f;
        }
        if (controller.GetButtonUp("RightHand")) {
            canRThrow = true;
        }
        if (controller.GetButtonUp("LeftHand")) {
            canLThrow = true;
        }
        pov.fieldOfView = tempFoV;
        Debug.Log(canLThrow + "," + lhCharging);
    }

    private void LateUpdate() {
        if (controller.GetButtonDown("RightHand") && canRThrow && rHand != null) {
            charging = true;
        }
        if (controller.GetButtonDown("LeftHand") && canLThrow && lHand != null) {
            lhCharging = true;
        }
    }
}
