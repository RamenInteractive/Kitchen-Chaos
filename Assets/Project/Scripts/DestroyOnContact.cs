using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collision collision) {
        if(collision.gameObject.transform.tag == "Food" || collision.gameObject.transform.tag == "Ingredient" || collision.gameObject.transform.tag == "Uncut" || collision.gameObject.transform.tag == "Uncooked") {
            Destroy(collision.gameObject);
        }
    }
}
