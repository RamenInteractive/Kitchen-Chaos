using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : IFood
{
    public static float[] ingFreq = { 0.0f, 0.1f, 0.4f, 0.6f, 0.8f, 1.0f};
    public enum Ingredient { TopBun, BottomBun, Burger, Cheese, Tomato, Lettuce };

    private List<int> ingredients;

    //Random constructor
    public Burger(int diffModifier)
    {
        int toppings = (int)(Random.value * (int)(diffModifier * 0.1 + 9) + 1);

        ingredients.Add((int)Ingredient.TopBun);
        ingredients.Add((int)Ingredient.BottomBun);

        for(int i = 0; i < toppings; i++)
        {
            float rand = Random.value;

            if (rand >= ingFreq[0] && rand <= ingFreq[1])
                ingredients.Add((int)Ingredient.BottomBun);
            else if (rand >= ingFreq[1] && rand <= ingFreq[2])
                ingredients.Add((int)Ingredient.Burger);
            else if (rand >= ingFreq[2] && rand <= ingFreq[3])
                ingredients.Add((int)Ingredient.Cheese);
            else if (rand >= ingFreq[3] && rand <= ingFreq[4])
                ingredients.Add((int)Ingredient.Tomato);
            else if (rand >= ingFreq[4] && rand <= ingFreq[5])
                ingredients.Add((int)Ingredient.Lettuce);
        }
    }
	
    public Burger(List<int> i)
    {
        ingredients = new List<int>(i);
    }

    public bool Compare(GameObject food)
    {
        Burger b = food.GetComponent<Burger>();

        if (b == null)
            return false;

        List<int> temp = new List<int>(b.ingredients);

        //for each ingredient in this burger find a match in temp and remove it
        foreach(int f in ingredients)
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
        return false;
    }
}
