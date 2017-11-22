using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : IFood
{
    public static int MAX_TOPPINGS = 4;
    public static Ingredient[] ingTypes = { new TopBun(), new BottomBun(), new Patty(), new Cheese(), new Tomato(), new Lettuce() };
    public static float[] ingFreq = { 0.0f, 0.1f, 0.4f, 0.6f, 0.8f, 1.0f};
    public long compareVal = 0;

    private List<Ingredient> ingredients;

    //Random constructor
    public Burger(float diffModifier)
    {
        ingredients = new List<Ingredient>();
        
        int toppings = Random.Range(1, (int)(MAX_TOPPINGS * diffModifier));
        int ingredientCount = ingTypes.Length;

        ingredients.Add(new TopBun());

        for(int i = 0; i < toppings; i++)
        {
            float rand = Random.value;
            for (int j = 0; j < ingredientCount; j++)
            {
                if (rand < ingFreq[j])
                {
                    ingredients.Add(ingTypes[j]);
                    break;
                }
            }
        }

        ingredients.Add(new BottomBun());

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
