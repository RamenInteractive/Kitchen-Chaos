using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyGame : Minigame
{
    public static int TOTAL_SLOTS = 8;
    public static int SELECT_SLOTS = 4;
    public Vector3 dropLocation = new Vector3(0.2f, 0.5f, 0f);
    public Text buildText;

    private Ingredient[] ingredientSlots;
    private int[] ingredientsStored;
    private Ingredient[] selectedSlots;
    private List<Ingredient> currentBuild;
    private int whichSide;
    
    private GameObject buildSpace;

    // Use this for initialization
    new void Start()
    {
        base.Start();

        whichSide = -1;

        ingredientsStored = new int[TOTAL_SLOTS];

        ingredientSlots = new Ingredient[TOTAL_SLOTS];

        selectedSlots = new Ingredient[SELECT_SLOTS];

        currentBuild = new List<Ingredient>();
        newBuildSpace();
    }

    public override void complete()
    {
        //Create food object
        Destroy(buildSpace.gameObject);
        newBuildSpace();

        currentBuild = new List<Ingredient>();
        updateHUD();
    }

    protected override void activeUpdate()
    {
        if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                selectSide(1);
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                selectSide(0);
            }
            else
            {
                selectSide(-1);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            selectSide(0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            selectSide(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            addIngredient(0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            addIngredient(1);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            addIngredient(2);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            addIngredient(3);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            complete();
        }
    }

    private void selectSide(int side)
    {
        if (side >= 0)
        {
            whichSide = side;
            int off = side * selectedSlots.Length;
            for (int x = 0; x < selectedSlots.Length; x++)
                selectedSlots[x] = ingredientSlots[x + off];
            string panelName = side == 0 ? "LeftItems" : "RightItems";
            string otherPanel = side == 1 ? "LeftItems" : "RightItems";
            HUD.transform.Find(panelName).transform.Find("BGPanel").GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.5f, 0.35f);
            HUD.transform.Find(otherPanel).transform.Find("BGPanel").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.35f);
        }
        else
        {
            for (int x = 0; x < selectedSlots.Length; x++)
            {
                selectedSlots[x] = null;
            }
            HUD.transform.Find("LeftItems").transform.Find("BGPanel").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.35f);
            HUD.transform.Find("RightItems").transform.Find("BGPanel").GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 0.35f);
        }
    }

    private void addIngredient(int slot)
    {
        if (selectedSlots[slot] != null)
        {
            int overallSlot = slot + (SELECT_SLOTS * whichSide);
            GameObject ingredient;

            ingredient = Instantiate(selectedSlots[slot].gameObject);

            currentBuild.Add(ingredient.gameObject.GetComponent<Ingredient>());

            if (--ingredientsStored[overallSlot] == 0)
            {
                selectedSlots[slot] = null;
                ingredientSlots[overallSlot] = null;
            }

            ingredient.transform.parent = buildSpace.transform;
            ingredient.transform.localPosition = dropLocation;
            ingredient.transform.rotation = Quaternion.identity;
            updateHUD();
        }
    }

    private void updateHUD()
    {
        /*string text = "Build:\n";
        foreach(Burger.Ingredient ingredient in currentBuild) {
            text += ingredient + "\n";
        }
        buildText.text = text;*/
    }

    private void newBuildSpace()
    {
        buildSpace = new GameObject();
        buildSpace.transform.parent = transform.parent.transform.Find("AssemblyTop").transform;
        buildSpace.transform.localPosition = Vector3.zero;
    }

    private bool storeIngredient(Ingredient ing)
    {
        int firstNull = -1;
        Type ingType = ing.GetType();

        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (firstNull != -1 && ingredientSlots[i] == null)
                firstNull = i;

            if(ingredientSlots[i] != null && ingredientSlots[i].GetType() == ingType)
            {
                ingredientsStored[i]++;
                return true;
            }
        }

        //Reaches here if there aren't any of the same ingredient type in the station
        if (firstNull != -1)
        {
            ingredientSlots[firstNull] = ing;
            ingredientsStored[firstNull]++;
            return true;
        }

        //If theres no room
        return false;
    }

    /**
     * handleItem
     * ==========
     * Checks the designated hand for an ingredient, then takes it from the player
     * and puts it in the station
     * 
     * @param: p - The player with the item
     * @param: leftHand - The hand you want to check
     */
    public override void handleItem(Player p, bool leftHand)
    {
        GameObject hand = leftHand ? p.lHand : p.rHand;
        Ingredient inHand = hand.GetComponent<Ingredient>();

        if (inHand == null)
            return;

        bool hasRoom = storeIngredient(inHand);

        if (!hasRoom)
            return;

        if (leftHand)
            p.lHand = null;
        else
            p.rHand = null;
    }

    public override void interact(GameObject caller, bool leftHand)
    {
        Player p = caller.GetComponent<Player>();

        if (p != null)
            return;

        GameObject hand = leftHand ? p.lHand : p.rHand;

        //If you aren't holding anything
        if (hand == null)
        {
            //check the opposite hand you interacted with for an ingredient
            if (leftHand)
                handleItem(p, false);
            else
                handleItem(p, true);

            enter(caller);
        }
        else //If you are holding something
        {
            //If it's an ingredient take it into the station
            if (hand.GetComponent<Ingredient>() != null)
            {
                if(leftHand)
                handleItem(p, true);
            else
                handleItem(p, false);
            }
        }
    }
}
