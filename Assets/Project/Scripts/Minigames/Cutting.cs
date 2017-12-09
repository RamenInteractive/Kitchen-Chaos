using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutting : Minigame {

    public Slider progressBar;
    public Transform cuttingBoard;
    public bool cutting;
    public GameObject lettucePrefab;
    public GameObject tomatoPrefab;
    public GameObject cheesePrefab;
    public GameObject friesPrefab;

    public List<Transform> storage;
    public List<bool> occupied;
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;
    public Transform slot4;

    // Use this for initialization
    new void Start () {
        storage = new List<Transform>();
        occupied = new List<bool>();
        progressBar.value = 0.0f;
        cutting = false;

        storage.Add(slot1);
        occupied.Add(false);
        storage.Add(slot2);
        occupied.Add(false);
        storage.Add(slot3);
        occupied.Add(false);
        storage.Add(slot4);
        occupied.Add(false);
        base.Start();
    }

    public override void complete()
    {
        
    }

    protected override void activeUpdate()
    {
        if (!cutting)
        {
            for (int i = 0; i < 4; i++)
            {
                if (occupied[i])
                {
                    storage[i].GetChild(0).transform.parent = cuttingBoard;
                    cuttingBoard.GetChild(1).gameObject.transform.localPosition = Vector3.zero;
                    cuttingBoard.GetChild(1).gameObject.GetComponent<Rigidbody>().useGravity = false;
                    cuttingBoard.GetChild(1).gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    cuttingBoard.GetChild(1).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    cutting = true;
                    occupied[i] = false;
                    progressBar.value = 0;
                    break;
                }
            }
        } else {
            if (controller.GetButtonDown("LeftHand") || controller.GetButtonDown("RightHand"))
            {
                progressBar.value = progressBar.value + .15f;
            }

            if (progressBar.value == 1)
            {
                List<GameObject> cuts = new List<GameObject>();
                GameObject cut1;
                GameObject cut2;
                GameObject cut3;
                GameObject cut4;
                GameObject cut5;

                if (cuttingBoard.GetChild(1).gameObject.name.Contains("UncutLettuce"))
                {
                    Destroy(cuttingBoard.GetChild(1).gameObject);
                    cut1 = Instantiate(lettucePrefab, cuttingBoard);
                    cut1.transform.localPosition = Vector3.zero;
                    cut2 = Instantiate(lettucePrefab, cuttingBoard);
                    cut2.transform.localPosition = Vector3.zero;
                    cut3 = Instantiate(lettucePrefab, cuttingBoard);
                    cut3.transform.localPosition = Vector3.zero;

                    cuts.Add(cut1);
                    cuts.Add(cut2);
                    cuts.Add(cut3);
                }
                else if (cuttingBoard.GetChild(0).gameObject.name.Contains("UncutTomato"))
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut1 = Instantiate(tomatoPrefab, cuttingBoard);
                    cut1.transform.localPosition = Vector3.zero;
                    cut2 = Instantiate(tomatoPrefab, cuttingBoard);
                    cut2.transform.localPosition = Vector3.zero;
                    cut3 = Instantiate(tomatoPrefab, cuttingBoard);
                    cut3.transform.localPosition = Vector3.zero;

                    cuts.Add(cut1);
                    cuts.Add(cut2);
                    cuts.Add(cut3);
                }
                else if (cuttingBoard.GetChild(1).gameObject.name.Contains("UncutCheese"))
                {
                    Destroy(cuttingBoard.GetChild(1).gameObject);
                    cut1 = Instantiate(cheesePrefab, cuttingBoard);
                    cut1.transform.localPosition = Vector3.zero;
                    cut2 = Instantiate(cheesePrefab, cuttingBoard);
                    cut2.transform.localPosition = Vector3.zero;
                    cut3 = Instantiate(cheesePrefab, cuttingBoard);
                    cut3.transform.localPosition = Vector3.zero;
                    cut4 = Instantiate(cheesePrefab, cuttingBoard);
                    cut4.transform.localPosition = Vector3.zero;
                    cut5 = Instantiate(cheesePrefab, cuttingBoard);
                    cut5.transform.localPosition = Vector3.zero;

                    cuts.Add(cut1);
                    cuts.Add(cut2);
                    cuts.Add(cut3);
                    cuts.Add(cut4);
                    cuts.Add(cut5);
                }
                else if (cuttingBoard.GetChild(1).gameObject.name.Contains("UncutPotato"))
                {
                    Destroy(cuttingBoard.GetChild(1).gameObject);
                    cut1 = Instantiate(friesPrefab, cuttingBoard);
                    cut1.transform.localPosition = Vector3.zero;
                    cut2 = Instantiate(friesPrefab, cuttingBoard);
                    cut2.transform.localPosition = Vector3.zero;
                    cut3 = Instantiate(friesPrefab, cuttingBoard);
                    cut3.transform.localPosition = Vector3.zero;

                    cuts.Add(cut1);
                    cuts.Add(cut2);
                    cuts.Add(cut3);
                }

                for (int i = 0; i < cuts.Count; i++)
                {
                    cuts[i].transform.parent = GameObject.Find("GameController").transform;
                    cuts[i].GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                    cuts[i].GetComponentInChildren<Rigidbody>().detectCollisions = true;
                    cuts[i].GetComponentInChildren<Rigidbody>().useGravity = true;
                    float angle = Random.Range(0, 360);
                    cuts[i].GetComponentInChildren<Rigidbody>().AddForce((new Vector3(Mathf.Cos(angle), 1, Mathf.Sin(angle))) * 250f);
                    
                    cutting = false;
                }
            }
        }
    }
        
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Uncut")
        {
            for (int i = 0; i < 4; i++)
            {
                if (!occupied[i])
                {
                    collision.gameObject.transform.parent = storage[i];
                    collision.gameObject.transform.localPosition = Vector3.zero;
                    collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    collision.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    occupied[i] = true;
                    break;
                }
            }
        }
    }

    public override void handleItem(Player p, bool leftHand)
    {
        GameObject hand = leftHand ? p.lHand : p.rHand;
        int i;

        if (hand == null)
            return;

        for (i = 0; i < 4; i++)
        {
            if (!occupied[i])
            {
                hand.gameObject.transform.parent = storage[i];
                hand.gameObject.transform.localPosition = Vector3.zero;
                hand.gameObject.GetComponent<Rigidbody>().useGravity = false;
                hand.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                hand.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                occupied[i] = true;
                break;
            }
        }

        if (i == 4)
            return;

        if (leftHand)
            p.lHand = null;
        else
            p.rHand = null;
        
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
            //If it's an ingredient take it into the station
            if (hand.tag == "Uncut")
            {
                handleItem(p, leftHand);
            }
        }
    }
}
