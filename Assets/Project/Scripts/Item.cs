using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {
    
    private float speed = 3.0f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
    }

    public override void interact(GameObject caller)
    {
        caller.GetComponent<Player>().pickUp(this.gameObject);
    }
}

