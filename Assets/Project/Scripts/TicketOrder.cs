using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketOrder
{
    private GameObject ticket;
    private IFood idealFood;

	public TicketOrder(GameObject ticket, IFood idealFood)
    {
        this.ticket = ticket;
        this.idealFood = idealFood;
    }

    private void CompleteOrder()
    {

    }
}
