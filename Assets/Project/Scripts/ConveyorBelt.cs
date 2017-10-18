using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour {

    public float speed = 3.0f;
    public GameObject belt;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag != "Food")
            return;

        float conveyorVelocity = speed * Time.deltaTime;
        collision.gameObject.transform.Translate(0, 0, conveyorVelocity, belt.transform);
    }
}
