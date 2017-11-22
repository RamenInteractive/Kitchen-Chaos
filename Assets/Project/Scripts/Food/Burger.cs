using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : IFood
{
    public static int MAX_TOPPINGS = 4;
    //public static Ingredient[] ingTypes = { new TopBun(), new BottomBun(), new Patty(), new Cheese(), new Tomato(), new Lettuce() };
    public static float[] ingFreq = { 0.0f, 0.1f, 0.4f, 0.6f, 0.8f, 1.0f};
    public long compareVal = 0;

    private enum BurgerIngredients {
        TopBun, BottomBun, Patty, Cheese, Tomato, Lettuce
    };
    private List<BurgerIngredients> ingredients;

    //Random constructor
    public Burger(float diffModifier)
    {
        ingredients = new List<BurgerIngredients>();

        int toppings;
        if (diffModifier >= MAX_TOPPINGS)
            toppings = MAX_TOPPINGS;
        else
            toppings = Random.Range((int)(diffModifier * (MAX_TOPPINGS - 1)) + 1, MAX_TOPPINGS);

        int ingredientCount = System.Enum.GetNames(typeof(BurgerIngredients)).Length;

        ingredients.Add(BurgerIngredients.TopBun);

        for(int i = 0; i < toppings; i++)
        {
            float rand = Random.value;
            for (int j = 0; j < ingredientCount; j++)
            {
                if (rand < ingFreq[j])
                {
                    ingredients.Add((BurgerIngredients)j);
                    break;
                }
            }
        }

        ingredients.Add(BurgerIngredients.BottomBun);

        generateCompareVal();
    }
	
    public Burger(List<Ingredient> i)
    {
        ingredients = new List<BurgerIngredients>();
        foreach(Ingredient ing in i) {
            ingredients.Add((BurgerIngredients)System.Enum.Parse(typeof(BurgerIngredients), ing.GetType().Name));
        }
        generateCompareVal();
    }

    public bool Compare(IFood food)
    {
        if (food.GetType() != GetType())
            return false;

        Burger b = (Burger)food;

        if (b == null)
            return false;

        return (compareVal == b.compareVal);
    }

    public string ingredientTicketList() {
        string output = "";

        foreach(BurgerIngredients ing in ingredients) {
            output += "- " + ing + "\n";
        }

        return output;
    }

    private void generateCompareVal() {
        compareVal = 0;
        foreach(BurgerIngredients ing in ingredients)
            compareVal += (long)System.Math.Pow(MAX_TOPPINGS, (int)ing);
    }
}
