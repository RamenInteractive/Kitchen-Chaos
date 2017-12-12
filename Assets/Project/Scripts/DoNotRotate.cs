using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotRotate : MonoBehaviour {
    Quaternion originalRotation;
	// Use this for initialization
	void Start () {
        transform.rotation *= Quaternion.Inverse(GetComponentInParent<Transform>().rotation);
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = originalRotation;
	}
}
