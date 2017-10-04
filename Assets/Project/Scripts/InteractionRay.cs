using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    public int length = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        string text;

        if (Physics.Raycast(ray, out hit, length))
            text = hit.collider.gameObject.GetComponent<Interactable>().Text;

    }
}
