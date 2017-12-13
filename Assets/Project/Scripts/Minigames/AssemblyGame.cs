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
    public Image upImage1;
    public Image upImage2;
    public Sprite upKeyboard;
    public Sprite upController;
    public Image downImage1;
    public Image downImage2;
    public Sprite downKeyboard;
    public Sprite downController;
    public Image leftImage1;
    public Image leftImage2;
    public Sprite leftKeyboard;
    public Sprite leftController;
    public Image rightImage1;
    public Image rightImage2;
    public Sprite rightKeyboard;
    public Sprite rightController;

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
        foreach(Transform t in buildSpace.transform) {
            t.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            t.gameObject.GetComponent<Rigidbody>().useGravity = false;
            t.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            t.gameObject.layer = 0;
            t.localRotation = Quaternion.identity;
        }
        buildSpace.AddComponent<Rigidbody>();
        buildSpace.AddComponent<BoxCollider>();
        buildSpace.AddComponent<FoodComponent>();
        buildSpace.GetComponent<FoodComponent>().food = new Burger(currentBuild);
        buildSpace.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.3f, 0.3f);
        buildSpace.layer = 12;
        buildSpace.tag = "Food";

        Player p = player.GetComponent<Player>();
        if(p.lHand == null) {
            p.pickUp(buildSpace, true);
        } else if(p.rHand == null) {
            p.pickUp(buildSpace, false);
        } else {
            buildSpace.transform.parent = gameObject.transform.parent;
        }

        newBuildSpace();

        currentBuild = new List<Ingredient>();
        updateHUD();
    }

    protected override void activeUpdate() {
        if(controller.GetButtonUp("RightHand") || controller.GetButtonUp("LeftHand")) {
            if(controller.GetButton("RightHand")) {
                selectSide(1);
            } else if(controller.GetButton("LeftHand")) {
                selectSide(0);
            } else {
                selectSide(-1);
            }
        }
        if(controller.GetButtonDown("LeftHand")) {
            selectSide(0);
        } else if(controller.GetButtonDown("RightHand")) {
            selectSide(1);
        }
        if(controller.GetAxisDown("MoveV") && controller.GetAxis("MoveV") > 0) {
            addIngredient(0);
        }
        if(controller.GetAxisDown("MoveH") && controller.GetAxis("MoveH") > 0) {
            addIngredient(1);
        }
        if(controller.GetAxisDown("MoveV") && controller.GetAxis("MoveV") < 0) {
            addIngredient(2);
        }
        if(controller.GetAxisDown("MoveH") && controller.GetAxis("MoveH") < 0) {
            addIngredient(3);
        }
        if(controller.GetButtonDown("Jump")) {
            complete();
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ingredient")
        {
            storeIngredient(collision.gameObject.GetComponent<Ingredient>());
            collision.gameObject.SetActive(false);
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

            ingredient.SetActive(true);
            ingredient.tag = "Food";
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
        for(int i = 0; i < TOTAL_SLOTS; i++) {
            string text = "[Empty]";
            if (ingredientSlots[i] != null)
                text = ingredientSlots[i].GetType().ToString() + " (" + ingredientsStored[i] + ")";

            GameObject.Find("TextSlot" + i).GetComponent<Text>().text = text;
        }
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
            if (firstNull == -1 && ingredientSlots[i] == null)
                firstNull = i;

            if(ingredientSlots[i] != null && ingredientSlots[i].GetType() == ingType)
            {
                ingredientsStored[i]++;
                sfx.playSelect();
                return true;
            }
        }

        //Reaches here if there aren't any of the same ingredient type in the station
        if (firstNull != -1)
        {
            ing.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            ing.GetComponent<Rigidbody>().detectCollisions = true;
            ing.GetComponent<Rigidbody>().useGravity = true;
            ingredientSlots[firstNull] = ing;
            ingredientsStored[firstNull]++;
            sfx.playSelect();
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
        updateHUD();

        GameObject hand = leftHand ? p.lHand : p.rHand;

        if (hand == null || hand.GetComponent<Ingredient>() == null || hand.tag != "Ingredient")
            return;

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


        inHand.gameObject.SetActive(false);
        updateHUD();
    }

    public override void interact(GameObject caller, bool leftHand)
    {
        Player p = caller.GetComponent<Player>();

        if (p == null)
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
            handleItem(p, leftHand);
        }
    }

    public override void enter(GameObject p)
    {
        base.enter(p);

        Player player = p.GetComponent<Player>();

        if (player.getKeyboard())
        {
            upImage1.sprite = upKeyboard;
            downImage1.sprite = downKeyboard;
            leftImage1.sprite = leftKeyboard;
            rightImage1.sprite = rightKeyboard;
            upImage2.sprite = upKeyboard;
            downImage2.sprite = downKeyboard;
            leftImage2.sprite = leftKeyboard;
            rightImage2.sprite = rightKeyboard;
        }
        else
        {
            upImage1.sprite = upController;
            downImage1.sprite = downController;
            leftImage1.sprite = leftController;
            rightImage1.sprite = rightController;
            upImage2.sprite = upController;
            downImage2.sprite = downController;
            leftImage2.sprite = leftController;
            rightImage2.sprite = rightController;
        }
    }
}
