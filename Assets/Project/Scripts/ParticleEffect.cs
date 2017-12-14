using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Detach() {
        ParticleSystem system = GetComponent<ParticleSystem>();
        transform.SetParent(null);
        system.Stop();
        Destroy(gameObject, system.main.startLifetime.constantMax);
    }
}
