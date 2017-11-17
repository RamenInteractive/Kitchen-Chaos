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
    public GameObject assemblyStation;
    public Transform tomatoPrefab;
    public Transform lettucePrefab;
    public Transform topBunPrefab;
    public Transform bottomBunPrefab;
    public Transform pattyPrefab;
    public Transform cheesePrefab;
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

    public override void handleItem(bool leftHand)
    {
        //DO STUFF HERE
    }

    public override void interact(GameObject caller, bool leftHand)
    {
        Player p = caller.GetComponent<Player>();
        if (p != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if (p.lHand == null)
                    base.interact(caller, leftHand);
                else
                    ;
            }
            else if(Input.GetMouseButtonDown(1))
            {
                if (p.rHand == null)
                    base.interact(caller, leftHand);
                else
                    ;
            }
        }
    }
}
