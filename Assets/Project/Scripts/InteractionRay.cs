using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    public float length = 4f;

    private Interactable hover;

	// Use this for initialization
	void Start ()
    {
        hover = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Interactable objectHit = null;

        if (Physics.Raycast(ray, out hit, length)) {
            objectHit = hit.collider.gameObject.GetComponent<Interactable>();
        }

        if(objectHit != hover) {
            if (hover != null && hover.HoverHUD != null) {
                hover.HoverHUD.GetComponent<CanvasGroup>().alpha = 0f;
                hover.hovering = false;
            }
            if (objectHit != null && objectHit.HoverHUD != null) {
                objectHit.HoverHUD.GetComponent<CanvasGroup>().alpha = 1f;
                objectHit.hovering = true;
            }
            hover = objectHit;
        } 

        //Attempts to interact with the object
        if((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && hover != null) {
            hover.interact(gameObject.transform.parent.gameObject);
        }
    }
}
