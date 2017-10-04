using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour {
    private bool inUse = false;

    protected List<Ingredient>[] ingredients; // Ingredient Storage
    protected Player player; // Player currently using minigame
    protected Controller controller; // Control scheme for the minigame

    public Camera viewpoint;
    public GameObject HUD;

    // Use this for initialization
    void Start () {
        HUD.SetActive(false);
        viewpoint.GetComponent<Camera>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /**
     * Enter
     * =====
     * 
     * Called to switch from player to minigame view and "check-out" this minigame
     * 
     * @param: p - Player wanting to enter this minigame
     */
    public void enter(Player p) {
        if(!inUse) {
            inUse = true;
            player = p;
            player.GetComponent<Camera>().enabled = false;
            viewpoint.GetComponent<Camera>().enabled = true;
            HUD.SetActive(true);
        }
    }

    /**
     * AddToStorage
     * ============
     * 
     * Add an ingredient to this minigame's ingredient storage
     * 
     * @param: ingredient - The ingredient to add to this minigame
     */
    public void addToStorage(Ingredient ingredient) {

    }

    /**
     * Complete
     * ========
     * 
     * Function that is called when this minigame's "task" is complete
     * 
     */ 
    public abstract void complete();

    /**
     * Exit
     * ====
     * 
     * Exit minigame and change perspective back to player
     * 
     */ 
    public void exit() {
        player.GetComponent<Camera>().enabled = true;
        viewpoint.GetComponent<Camera>().enabled = false;
        HUD.SetActive(false);
        player = null;
        inUse = false;
    }
}
