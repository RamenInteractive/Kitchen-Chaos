using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingGame : Minigame {

    public Text counter;
    public Slider progressBar;
    public GameObject uncut;
    public GameObject cut;
    public int increase;
    public int chop;

    // Use this for initialization
    new void Start () {
        base.Start();
        
        counter.text = "0";
        chop = 0;
        increase = 0;
        progressBar.value = 0;
    }

	// Update is called once per frame
	new void Update () {
        base.Update();
        uncut.GetComponent<MeshRenderer>().enabled = true;
        cut.GetComponent<MeshRenderer>().enabled = false;

        if (Minigame.inUse)
        {
            progressBar.GetComponent<CanvasGroup>().alpha = 1f;
            
            if (Input.GetKeyDown("space"))
            {
                progressBar.value = progressBar.value + .15f;
            }

            if (progressBar.value >= .5f)
            {
                uncut.GetComponent<MeshRenderer>().enabled = false;
                cut.GetComponent<MeshRenderer>().enabled = true;
            }

            if (progressBar.value == 1)
            {
                uncut.GetComponent<MeshRenderer>().enabled = true;
                cut.GetComponent<MeshRenderer>().enabled = false;
                chop++;
                counter.text = chop.ToString();
                progressBar.value = 0;
                
            }
        }

        if (!Minigame.inUse)
        {
            progressBar.GetComponent<CanvasGroup>().alpha = 0f;
        }
	}

    public override void complete() {

    }
}
