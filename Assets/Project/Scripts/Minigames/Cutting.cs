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
                    cuttingBoard.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
                    cuttingBoard.GetChild(0).gameObject.GetComponent<Rigidbody>().useGravity = false;
                    cuttingBoard.GetChild(0).gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                    cuttingBoard.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
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
                GameObject cut;
                if (cuttingBoard.GetChild(0).gameObject.name.Contains("UncutLettuce"))
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(lettucePrefab, cuttingBoard);
                    cut.transform.localPosition = Vector3.zero;
                } else if (cuttingBoard.GetChild(0).gameObject.name.Contains("UncutTomato"))
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(tomatoPrefab, cuttingBoard);
                    cut.transform.localPosition = Vector3.zero;
                } else if (cuttingBoard.GetChild(0).gameObject.name.Contains("UncutCheese"))
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(cheesePrefab, cuttingBoard);
                    cut.transform.localPosition = Vector3.zero;
                } else {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(friesPrefab, cuttingBoard);
                    cut.transform.localPosition = Vector3.zero;
                }

                cut.transform.parent = GameObject.Find("GameController").transform;
                cut.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                cut.GetComponentInChildren<Rigidbody>().detectCollisions = true;
                cut.GetComponentInChildren<Rigidbody>().useGravity = true;
                float angle = Random.Range(0, 360);
                cut.GetComponentInChildren<Rigidbody>().AddForce((new Vector3(Mathf.Cos(angle), 1,Mathf.Sin(angle))) * 250f);
                cutting = false;
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

        if (hand == null)
            return;

        Ingredient inHand = hand.GetComponent<Ingredient>();

        if (inHand == null)
            return;

        for (int i = 0; i < 4; i++)
        {
            if (!occupied[i])
            {
                inHand.gameObject.transform.parent = storage[i];
                inHand.gameObject.transform.localPosition = Vector3.zero;
                inHand.gameObject.GetComponent<Rigidbody>().useGravity = false;
                inHand.gameObject.GetComponent<Rigidbody>().detectCollisions = false;
                inHand.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                occupied[i] = true;
                break;
            }
        }

        inHand.gameObject.SetActive(false);
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
            if (hand.GetComponent<Ingredient>() != null)
            {
                handleItem(p, leftHand);
            }
        }
    }
}
