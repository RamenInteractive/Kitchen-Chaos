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
    public GameObject tomatoPrefab;
    public GameObject lettucePrefab;
    public Transform completedTransform;
    private GameObject newCut;
    public int chop;

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
            base.interact(this.gameObject);
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
                Destroy(Player.rHand.gameObject);
                if (holding.name == "UncutLettuce")
                {
                    newCut = Instantiate(lettucePrefab, completedTransform);
                    newCut.transform.localPosition = Vector3.zero;
                    newCut.transform.GetComponent<Rigidbody>().useGravity = false;
                    newCut.transform.GetComponent<Rigidbody>().detectCollisions = false;
                    newCut.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    Player.rHand = newCut;
                }
                else if (holding.name == "UncutTomato")
                {
                    Instantiate(tomatoPrefab, completedTransform);
                    newCut.transform.localPosition = Vector3.zero;
                    newCut.transform.GetComponent<Rigidbody>().useGravity = false;
                    newCut.transform.GetComponent<Rigidbody>().detectCollisions = false;
                    newCut.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    Player.rHand = newCut;
                }
                exit();
                Debug.Log("Holding this" + Player.rHand);
                counter.text = chop.ToString();
                progressBar.value = 0;

            }
        } else if (holding == null)
        {
            interact(holding);
        }
    }

    public override void interact(GameObject obj)
    {
        HoverHUD.GetComponentInChildren<Text>().text = "Pick up ingredient";
    }
}
