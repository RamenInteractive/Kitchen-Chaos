﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Minigame : Interactable {

    protected List<Ingredient> ingredients; // Ingredient Storage
    protected GameObject player; // Player currently using minigame
    protected Controller controller; // Control scheme for the minigame

    private bool inUse = false;
    public Camera viewpoint;
    public GameObject HUD;
    private string hud;

    // Use this for initialization
    protected void Start () {
        viewpoint.GetComponent<Camera>().enabled = false;
        ingredients = new List<Ingredient>();
        hud = HoverHUD.GetComponentInChildren<Text>().text;
	}
	
	// Update is called once per frame
	protected void Update () {
        if(inUse) {
            if (Input.GetKeyDown(KeyCode.X)) {
                exit();
            }
            activeUpdate();
        }
    }

    protected abstract void activeUpdate();

    /**
     * enter
     * =====
     * Called to switch from player to minigame view and "check-out" this minigame
     * 
     * @param: p - Player wanting to enter this minigame
     */
    public virtual void enter(GameObject p) {
        if(!inUse) {
            HoverHUD.GetComponentInChildren<Text>().text = hud;
            inUse = true;
            player = p;
            player.transform.Find("FirstPersonCharacter").gameObject.GetComponent<Camera>().enabled = false;
            viewpoint.GetComponent<Camera>().enabled = true;
            gameObject.GetComponent<Interactable>().HoverHUD.GetComponent<CanvasGroup>().alpha = 0f;
            GameObject.Find("CrossHair").GetComponent<CanvasGroup>().alpha = 0f;
            HUD.GetComponent<CanvasGroup>().alpha = 1f;
            controller = player.GetComponent<Controller>();

            CustomFirstPersonController cfpc = player.GetComponent<CustomFirstPersonController>();
            Player playerScript = player.GetComponent<Player>();

            cfpc.enabled = false;
            playerScript.enabled = false;

            //player.SetActive(false);
        }
    }

    /**
     * addToStorage
     * ============
     * Add an ingredient to this minigame's ingredient storage
     * 
     * @param: ingredient - The ingredient to add to this minigame
     */
    public void addToStorage(Ingredient ingredient)
    {

    }

    /**
     * complete
     * ========
     * Function that is called when this minigame's "task" is complete
     */ 
    public abstract void complete();

    /**
     * exit
     * ====
     * Exit minigame and change perspective back to player
     */ 
    public void exit() {
        CustomFirstPersonController cfpc = player.GetComponent<CustomFirstPersonController>();
        Player playerScript = player.GetComponent<Player>();

        cfpc.enabled = true;
        playerScript.enabled = true;

        //player.SetActive(true);
        
        player.transform.Find("FirstPersonCharacter").gameObject.GetComponent<Camera>().enabled = true;
        viewpoint.GetComponent<Camera>().enabled = false;
        HUD.GetComponent<CanvasGroup>().alpha = 0f;
        gameObject.GetComponent<Interactable>().HoverHUD.GetComponent<CanvasGroup>().alpha = 1f;
        GameObject.Find("CrossHair").GetComponent<CanvasGroup>().alpha = 1f;
        player = null;
        inUse = false;
    }

    /**
     * handleItem
     * ====
     * Processes the item held in the designated hand of the caller
     * 
     * @param: p - The player with the item
     * @param: leftHand - The hand you want to check
     */
    public virtual void handleItem(Player p, bool leftHand)
    {

    }

    public override void interact(GameObject caller, bool leftHand)
    {
        Player p = caller.GetComponent<Player>();

        if (p != null)
            return;

        enter(caller);
        handleItem(p, leftHand);
    }
}
