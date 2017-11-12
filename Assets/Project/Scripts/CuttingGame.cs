using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingGame : Minigame {

    public Text counter;
    public Slider progressBar;
    public GameObject uncut;
    public GameObject cut;
    public GameObject chopped;
    public int chop;
    private bool ingredientUse;

    // Use this for initialization
    new void Start () {
        base.Start();
        
        counter.text = "0";
        chop = 0;
        progressBar.value = 0;
    }

    public override void complete() {

    }

    protected override void activeUpdate() {
        if (holding.name == "UncutTomato" || holding.name == "UncutLettuce")
        {
            if (Input.GetKeyDown("space"))
            {
                progressBar.value = progressBar.value + .15f;
            }

            if (progressBar.value >= .25f)
            {
                uncut.GetComponent<MeshRenderer>().enabled = false;
                cut.GetComponent<MeshRenderer>().enabled = true;
                chopped.GetComponent<MeshRenderer>().enabled = false;
            }

            if (progressBar.value >= .60f)
            {
                uncut.GetComponent<MeshRenderer>().enabled = false;
                cut.GetComponent<MeshRenderer>().enabled = false;
                chopped.GetComponent<MeshRenderer>().enabled = true;
            }

            if (progressBar.value == 1)
            {
                uncut.GetComponent<MeshRenderer>().enabled = true;
                cut.GetComponent<MeshRenderer>().enabled = false;
                chopped.GetComponent<MeshRenderer>().enabled = false;
                chop++;
                counter.text = chop.ToString();
                progressBar.value = 0;

            }
        }
    }
}
