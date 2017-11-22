using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketOrder
{
    private int orderNumber;
    private GameObject ticket;
    private IFood idealFood;

	public TicketOrder(GameObject ticket, IFood idealFood, int num)
    {
        this.ticket = ticket;
        this.idealFood = idealFood;
        orderNumber = num;

        ticket.transform.Find("OrderNumber").GetComponent<TextMesh>().text = "ORDER: #" + num;
        ticket.transform.Find("OrderType").GetComponent<TextMesh>().text = idealFood.GetType().Name;
        ticket.transform.Find("OrderIngredients").GetComponent<TextMesh>().text = idealFood.ingredientTicketList();
    }

    public int getOrderNum()
    {
        return orderNumber;
    }

    private void CompleteOrder()
    {

    }
}
