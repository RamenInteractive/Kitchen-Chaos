using UnityEngine;
using UnityEngine.UI;

public class InteractionRay : MonoBehaviour
{
    public float length;

    private Interactable hover;
    private GameObject parent;
    private InteractionRay ray;
    private Controller controller;
    private Player player;

    // Use this for initialization
    void Start ()
    {
        hover = null;
        parent = gameObject.transform.parent.gameObject;
        controller = parent.GetComponent<Controller>();
        player = parent.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!controller)
        {
            controller = parent.GetComponent<Controller>();
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        int itemLayer = 1 << 12;
        int gameLayer = 1 << 13;

        Interactable objectHit = null;

        if (Physics.Raycast(ray, out hit, length, gameLayer)) {
            objectHit = hit.collider.gameObject.GetComponent<Interactable>();
        }
        if (Physics.Raycast(ray, out hit, length, itemLayer)) {
            objectHit = hit.collider.gameObject.GetComponent<Interactable>();
        }

        if(objectHit != hover) {
            if (hover != null && hover.HoverHUD != null) {
                player.stopHover();
            }
            if (objectHit != null && objectHit.HoverHUD != null) {
                Transform t = objectHit.HoverHUD.transform.GetChild(0);
                player.startHover(t.GetComponent<Text>().text);
            }
            hover = objectHit;
        } 

        if(controller.GetButtonDown("LeftHand") && hover != null) {
            hover.interact(parent, true);
        }

        if (controller.GetButtonDown("RightHand") && hover != null) {
            hover.interact(parent, false);
        }
    }
}
