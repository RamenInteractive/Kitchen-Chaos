using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlippingGame : Minigame { 

    public Text counter;
    public GameObject burger1;
    public GameObject timer1;
    public bool slot1;
    public bool flipped1;

    public GameObject burger2;
    public GameObject timer2;
    public bool slot2;
    public bool flipped2;

    private static float time1;
    private static float time2;

    public int cooked;

    // Use this for initialization
    new void Start () {
        base.Start();
        burger1.GetComponent<MeshRenderer>().enabled = false;
        timer1.GetComponent<TextMesh>().color = Color.green;
        timer1.GetComponent<TextMesh>().text = "Open";
        slot1 = false;

        burger2.GetComponent<MeshRenderer>().enabled = false;
        timer2.GetComponent<TextMesh>().color = Color.green;
        timer2.GetComponent<TextMesh>().text = "Open";
        slot2 = false;

        cooked = 0;
    }
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
        //progressBar.GetComponent<CanvasGroup>().alpha = 1f;

        if (inUse)
        {
            if (!slot1 && Input.GetKeyDown("q"))
            {
                burger1.GetComponent<MeshRenderer>().enabled = true;
                timer1.GetComponent<TextMesh>().color = Color.green;
                slot1 = true;
                flipped1 = false;
                time1 = 15f;
            }

            if (time1 < 10f && !flipped1 && Input.GetKeyDown("a"))
            {
                time1 = 15f;
                flipped1 = true;
            }

            if (time1 < 10f && flipped1 && Input.GetKeyDown("a"))
            {
                burger1.GetComponent<MeshRenderer>().enabled = false;
                burger1.GetComponent<MeshRenderer>().enabled = true;
                timer1.GetComponent<TextMesh>().color = Color.green;
                timer1.GetComponent<TextMesh>().text = "Open";
                flipped1 = false;
                slot1 = false;
                burger1.GetComponent<MeshRenderer>().enabled = false;
                cooked++;
            }

            if (!slot2 && Input.GetKeyDown("e"))
            {
                burger2.GetComponent<MeshRenderer>().enabled = true;
                timer2.GetComponent<TextMesh>().color = Color.green;
                slot2 = true;
                time2 = 15f;
            }

            if (time2 < 10f && !flipped2 && Input.GetKeyDown("d"))
            {
                time2 = 15f;
                flipped2 = true;
            }

            if (time2 < 10f && flipped2 && Input.GetKeyDown("d"))
            {
                burger2.GetComponent<MeshRenderer>().enabled = false;
                burger2.GetComponent<MeshRenderer>().enabled = true;
                timer2.GetComponent<TextMesh>().color = Color.green;
                timer2.GetComponent<TextMesh>().text = "Open";
                flipped2 = false;
                slot2 = false;
                burger2.GetComponent<MeshRenderer>().enabled = false;
                cooked++;
            }

        }

        if (slot1)
        {
            time1 -= Time.deltaTime;
            timer1.GetComponent<TextMesh>().text = Mathf.Round(time1).ToString();
            
            if (time1 < 10f)
            {
                timer1.GetComponent<TextMesh>().color = Color.red;
            }

            if (time1 < 0)
            {
                timer1.GetComponent<TextMesh>().text = "Burnt";
                burger1.GetComponent<MeshRenderer>().enabled = false;
                flipped1 = false;
                slot1 = false;
            }

        }

        if (slot2)
        {
            time2 -= Time.deltaTime;
            timer2.GetComponent<TextMesh>().text = Mathf.Round(time2).ToString();

            if (time2 < 10f)
            {
                timer2.GetComponent<TextMesh>().color = Color.red;
            }
            if (time2 < 0f)
            {
                timer2.GetComponent<TextMesh>().text = "Burnt";
                burger2.GetComponent<MeshRenderer>().enabled = false;
                flipped2 = false;
                slot2 = false;
            }
        }

        counter.text = cooked.ToString();
    }

    public override void complete()
    {

    }
}
