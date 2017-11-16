using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public enum Ingredient { Bun, Cheese, UncookedPatty, Patty, UncutTomato, Tomato, UncutLettuce,  Lettuce };
    private bool cooked;
    private bool chopped;
    public static Ingredient type;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
        if (this.gameObject.name == "Bun") { 
            type = Ingredient.Bun;
        }

        if (this.gameObject.name == "Cheese")
        {
            type = Ingredient.Cheese;
        }

        if (this.gameObject.name == "UncookedPatty")
        {
            cooked = false;
            type = Ingredient.UncookedPatty;
        }

        if (this.gameObject.name == "Patty")
        {
            cooked = true;
            type = Ingredient.Patty;
        }

        if (this.gameObject.name == "UncutLettuce")
        {
            chopped = false;
            type = Ingredient.UncutLettuce;
        }

        if (this.gameObject.name == "Lettuce")
        {
            chopped = true;
            type = Ingredient.Lettuce;
        }

        if (this.gameObject.name == "UncutTomato")
        {
            chopped = false;
            type = Ingredient.UncutTomato;
        }

        if (this.gameObject.name == "Tomato")
        {
            chopped = true;
            type = Ingredient.Tomato;
        }
    }

    public override void interact(GameObject caller, bool leftHand)
    {
        caller.GetComponent<Player>().pickUp(this.gameObject, leftHand);
    }
}
