using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipping : Minigame {

    public GameObject pattyPrefab;
    public List<Transform> cooking;
    public List<GameObject> timers;
    public List<bool> occupied;
    public List<bool> flipped;

    public Transform slot1;
    public GameObject timer1;
    public Transform slot2;
    public GameObject timer2;
    public Transform slot3;
    public GameObject timer3;
    public Transform slot4;
    public GameObject timer4;
    public Transform slot5;
    public GameObject timer5;
    public Transform slot6;
    public GameObject timer6;

    // Use this for initialization
    new void Start () {
        cooking = new List<Transform>();
        timers = new List<GameObject>();
        occupied = new List<bool>();
        flipped = new List<bool>();

        cooking.Add(slot1);
        timers.Add(timer1);
        occupied.Add(false);
        flipped.Add(false);

        cooking.Add(slot2);
        timers.Add(timer2);
        occupied.Add(false);
        flipped.Add(false);

        cooking.Add(slot3);
        timers.Add(timer3);
        occupied.Add(false);
        flipped.Add(false);

        cooking.Add(slot4);
        timers.Add(timer4);
        occupied.Add(false);
        flipped.Add(false);

        cooking.Add(slot5);
        timers.Add(timer5);
        occupied.Add(false);
        flipped.Add(false);

        cooking.Add(slot6);
        timers.Add(timer6);
        occupied.Add(false);
        flipped.Add(false);
        base.Start();
    }

    public void update()
    {

    }

    public override void complete()
    {
        
    }

    protected override void activeUpdate()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit1");
        if (collision.gameObject.tag == "Uncooked")
        {
            Debug.Log("Hit2");
            for (int i = 0; i < 6; i++)
            {
                if (!occupied[i])
                {
                    collision.gameObject.transform.parent = cooking[i];
                    collision.gameObject.transform.localPosition = Vector3.zero;
                    collision.gameObject.transform.rotation = Quaternion.identity;
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
