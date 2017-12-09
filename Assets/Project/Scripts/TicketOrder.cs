using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketOrder
{
    private int orderNumber;
    private GameObject ticket;
    private IFood idealFood;
    private GameTime startTime;

	public TicketOrder(GameObject ticket, IFood idealFood, int num, GameTime startTime)
    {
        this.ticket = ticket;
        this.idealFood = idealFood;
        this.startTime = startTime;
        orderNumber = num;

        ticket.transform.Find("OrderNumber").GetComponent<TextMesh>().text = "ORDER: #" + num;
        ticket.transform.Find("OrderType").GetComponent<TextMesh>().text = idealFood.GetType().Name;
        ticket.transform.Find("OrderIngredients").GetComponent<TextMesh>().text = idealFood.ingredientTicketList();
    }

    public int getOrderNum()
    {
        return orderNumber;
    }

    public GameObject getTicket()
    {
        return ticket;
    }

    public GameTime getStartTime()
    {
        return startTime;
    }

    public IFood getFood()
    {
        return idealFood;
    }
}
