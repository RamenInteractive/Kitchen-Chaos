﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketGen : MonoBehaviour
{
    public const int NUM_TICKETS = 12;
    public const int FOOD_TYPES = 1;
    public const int TICKET_DURATION = 240;
    public static Vector3[] positions = { new Vector3(0.105f, 1.6f, -2.4f),  //top far left
        new Vector3(0.105f, 1.6f, -0.8f), new Vector3(0.105f, 1.6f, 0.8f),   //top mid left, top mid right
        new Vector3(0.105f, 1.6f, 2.4f), new Vector3(0.105f, 0f, -2.4f),     //top far right, middle far left
        new Vector3(0.105f, 0f, -0.8f), new Vector3(0.105f, 0f, 0.8f),       //middle mid left, middle mid right
        new Vector3(0.105f, 0f, 2.4f), new Vector3(0.105f, -1.6f, -2.4f),    //middle far right, bottom far left
        new Vector3(0.105f, -1.6f, -0.8f), new Vector3(0.105f, -1.6f, 0.8f), //bottom mid left, bottom mid right
        new Vector3(0.105f, -1.6f, 2.4f)};                                   //bottom far right

    public float diffModifier = 1;
    public GameObject ticket;
    public TicketOrder[] tickets;
    private int tixSoFar;
    private GameSession myGame;

    // Use this for initialization
    protected void Start()
    {
        tickets = new TicketOrder[NUM_TICKETS];
        myGame = transform.parent.GetComponent<GameSession>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown("c"))
            newOrder();

        for(int i = 0; i < tickets.Length; i++)
        {
            if(tickets[i] != null && !tickets[i].UpdateTime(myGame.getTime()))
            {
                failTicket(i);
            }
        }
    }

    public bool checkOrders(IFood food)
    {
        int bestMatch = -1;
        for(int i = 0; i < tickets.Length; i++)
        {
            //if already found an order thats been waiting longer keep that one
            if (tickets[i] == null || food.GetType() != tickets[i].getFood().GetType()
                || (bestMatch != -1 && tickets[bestMatch].getStartTime() < tickets[i].getStartTime()))
                continue;

            if (food.Compare(tickets[i].getFood()))
                bestMatch = i;
        }

        if(bestMatch != -1)
        {
            StartCoroutine(myGame.finishOrder(tickets[bestMatch].getTimeSpent()));
            deleteTicket(bestMatch);
            return true;
        } else {
            StartCoroutine(myGame.failOrder());
            return false;
        }
    }

    private void failTicket(int i) {
        deleteTicket(i);
        myGame.loseLife();
    }

    private void deleteTicket(int i)
    {
        Ticket t = tickets[i].getTicket().GetComponent<Ticket>();
        t.removeParticleEffects();
        Destroy(tickets[i].getTicket());
        tickets[i] = null;
    }

    public void newOrder()
    {
        for (int i = 0; i < NUM_TICKETS; i++)
        {
            if (tickets[i] != null)
                continue;
            GameObject instTicket = Instantiate(ticket);
            instTicket.transform.parent = transform;
            instTicket.transform.localPosition = positions[i];
            instTicket.transform.localRotation = Quaternion.identity;

            TicketOrder order = null;
            int randFood = Random.Range(0, FOOD_TYPES);

            switch (randFood)
            {
                case 0:
                    order = new TicketOrder(instTicket, new Burger(diffModifier), ++tixSoFar, myGame.getTime());
                    break;
            }

            tickets[i] = order;
            break;
        }
    }

    public int numOrders() {
        int sum = 0;
        foreach(TicketOrder to in tickets) {
            if (to != null)
                sum++;
        }
        return sum;
    }

    public void resetTickets()
    {
        for(int i = 0; i < tickets.Length; i++) {
            if(tickets[i] != null) {
                deleteTicket(i);
            }
        }
        tickets = new TicketOrder[NUM_TICKETS];
        tixSoFar = 0;
    }
}
