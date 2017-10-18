using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketGen : MonoBehaviour
{
    public const int NUM_TICKETS = 12;
    public const int FOOD_TYPES = 1;
    public static Vector3[] positions = { new Vector3(0.105f, 0.8f, -1.2f),   //top far left
        new Vector3(0.105f, 0.8f, -0.4f), new Vector3(0.105f, 0.8f, 0.4f),   //top mid left, top mid right
        new Vector3(0.105f, 0.8f, 1.2f), new Vector3(0.105f, 0f, -1.2f),     //top far right, middle far left
        new Vector3(0.105f, 0f, -0.4f), new Vector3(0.105f, 0f, 0.4f),       //middle mid left, middle mid right
        new Vector3(0.105f, 0f, 1.2f), new Vector3(0.105f, -0.8f, -1.2f),    //middle far right, bottom far left
        new Vector3(0.105f, -0.8f, -0.4f), new Vector3(0.105f, -0.8f, 0.4f), //bottom mid left, bottom mid right
        new Vector3(0.105f, -0.8f, 1.2f)};                                   //bottom far right

    public float diffModifier = 0;
    public GameObject ticket;
    public ArrayList tickets;

	// Use this for initialization
	void Start ()
    {
        tickets = new ArrayList();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("c") && tickets.Count < NUM_TICKETS)
        {
            GameObject instTicket = Instantiate(ticket);
            instTicket.transform.parent = transform;
            instTicket.transform.localPosition = positions[tickets.Count];
            tickets.Add(NewOrder(instTicket));
        }
    }

    TicketOrder NewOrder(GameObject ticket)
    {
        TicketOrder order = null;
        int randFood = (int)Random.value * (FOOD_TYPES - 1);

        switch (randFood)
        {
            case 0: order = new TicketOrder(ticket, new Burger(diffModifier));
                    break;
        }
        
        return order;
    }
}
