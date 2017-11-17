using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public GameObject HoverHUD;
    public bool hovering;

    public abstract void interact(GameObject caller, bool leftHand);
}
