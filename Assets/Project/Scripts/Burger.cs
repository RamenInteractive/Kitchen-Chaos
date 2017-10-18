using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : IFood
{
    public static int MAX_TOPPINGS = 10;

    public static float[] ingFreq = { 0.0f, 0.1f, 0.4f, 0.6f, 0.8f, 1.0f};
    public enum Ingredient { TopBun, BottomBun, Patty, Cheese, Tomato, Lettuce };
    public long compareVal = 0;

    private List<Ingredient> ingredients;

    //Random constructor
    public Burger(float diffModifier)
    {
        ingredients = new List<Ingredient>();
        //int toppings = (int)(Random.value * (int)(diffModifier * 0.1 + MAX_TOPPINGS - 1) + 1);
        int toppings = Random.Range(1, (int)(MAX_TOPPINGS * diffModifier));
        int ingredientCount = System.Enum.GetNames(typeof(Ingredient)).Length;

        ingredients.Add(Ingredient.TopBun);

        for(int i = 0; i < toppings; i++)
        {
            float rand = Random.value;
            for (int j = 0; j < ingredientCount; j++)
                if (rand < ingFreq[j])
                    ingredients.Add((Ingredient)j);
        }

        ingredients.Add(Ingredient.BottomBun);

        generateCompareVal();
    }
	
    public Burger(List<Ingredient> i)
    {
        ingredients = new List<Ingredient>(i);
        generateCompareVal();
    }

    public bool Compare(GameObject food)
    {
        Burger b = food.GetComponent<Burger>();

        if (b == null)
            return false;

        return (compareVal == b.compareVal);

        /*List<Ingredient> temp = new List<Ingredient>(b.ingredients);

        //for each ingredient in this burger find a match in temp and remove it
        foreach(Ingredient f in ingredients)
        {
            bool found = false;
            for(int i = 0; i < temp.Count && !found; i++)
            {
                if (f == temp[i])
                {
                    found = true;
                    temp.RemoveAt(i);
                }
            }
            //if no match is found for an ingredient, the burgers are not the same
            if (!found)
                return false;
        }

        //if there was an unremoved ingredient in them, the burgers are not the same
        if (temp.Count == 0)
            return true;
        return false;*/
    }

    public string ingredientTicketList() {
        string output = "";

        foreach(Ingredient ing in ingredients) {
            output += "- " + ing + "\n";
        }

        return output;
    }

    private void generateCompareVal() {
        compareVal = 0;
        for(int i = 0; i < ingredients.Count; i++)
            compareVal += (long)System.Math.Pow(MAX_TOPPINGS, i);
    }
}
