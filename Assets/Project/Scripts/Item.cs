using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {

    private bool holding;

    // Use this for initialization
    void Start () {
        holding = false;
	}

    // Update is called once per frame
    void Update() {
    }

    public override void interact(GameObject caller)
    {
        caller.GetComponent<Player>().pickUp(this.gameObject);
    }
}

