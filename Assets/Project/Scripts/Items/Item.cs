using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    private bool cooked;
    private bool chopped;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void interact(GameObject caller, bool leftHand)
    {
        caller.GetComponent<Player>().pickUp(this.gameObject, leftHand);
    }
}
