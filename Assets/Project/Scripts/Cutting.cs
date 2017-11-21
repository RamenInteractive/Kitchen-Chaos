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
            if (Input.GetKeyDown("space"))
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
                } else if (cuttingBoard.GetChild(0).gameObject.name.Contains("UncutTomato"))
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(tomatoPrefab, cuttingBoard);
                } else
                {
                    Destroy(cuttingBoard.GetChild(0).gameObject);
                    cut = Instantiate(cheesePrefab, cuttingBoard);
                }

                cut.transform.parent = GameObject.Find("GameController").transform;
                cut.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.None;
                cut.GetComponentInChildren<Rigidbody>().detectCollisions = true;
                cut.GetComponentInChildren<Rigidbody>().useGravity = true;
                cut.GetComponentInChildren<Rigidbody>().AddForce((transform.up) * 250f);
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
}
