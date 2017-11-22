﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitOrder : MonoBehaviour
{
    public TicketGen board;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IFood>() != null)
        {
            IFood food = collision.gameObject.GetComponent<IFood>();

            board.checkOrders(food);
        }
    }
}
