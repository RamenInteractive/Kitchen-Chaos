using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    public float length = 3f;

    private Interactable hover;
    public static bool touching;

	// Use this for initialization
	void Start ()
    {
        hover = null;
        touching = false;
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
            if (hover != null) {
                hover.HoverHUD.GetComponent<CanvasGroup>().alpha = 0f;
            }
            if (objectHit != null) {
                objectHit.HoverHUD.GetComponent<CanvasGroup>().alpha = 1f;
            }
            hover = objectHit;
            touching = true;
        } 

        if(Input.GetMouseButtonDown(0) && hover != null) {
            hover.interact(gameObject.transform.parent.gameObject);
        }
    }
}
