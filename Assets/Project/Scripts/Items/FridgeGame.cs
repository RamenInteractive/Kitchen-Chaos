using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeGame : Minigame
{
    // The container holding all of the fridge items
    public GameObject fridgeContainer;

    // directions pressed. Order: right, left, up, down
    private bool[] dirsPressed;

    // The currently selected object within the object list
    private GameObject selectedObject;

    public GameObject lettucePrefab;
    public GameObject potatoPrefab;
    public GameObject tomatoPrefab;
    public GameObject pattyPrefab;
    public GameObject cheesePrefab;
    public GameObject btmBunPrefab;
    public GameObject topBunPrefab;

    new protected void Start() {
        base.Start();
        dirsPressed = new bool[4];
    }

    protected override void activeUpdate() {
        float hAxis = controller.GetAxis("MoveH");
        float vAxis = controller.GetAxis("MoveV");
        // Input checking: Check the stick/keyboard/d-pad for input
        // May later be replaced by a more robust input system

        // Check the horizontal axis direction
        if (hAxis > 0.3 && !dirsPressed[0]) {
            MoveCursor(0);
            dirsPressed[0] = true;
            dirsPressed[1] = false;
        } else if (hAxis < -0.3 && !dirsPressed[1]) {
            MoveCursor(Mathf.PI);
            dirsPressed[0] = false;
            dirsPressed[1] = true;
        } else if (hAxis < 0.3 && hAxis > -0.3) {
            // Horizontal axis is inside of the deadzone
            dirsPressed[0] = false;
            dirsPressed[1] = false;
        }

        // Check the vertical axis direction
        if (vAxis > 0.3 && !dirsPressed[2]) {
            MoveCursor(Mathf.PI * 0.5f);
            dirsPressed[2] = true;
            dirsPressed[3] = false;
        } else if (vAxis < -0.3 && !dirsPressed[3]) {
            MoveCursor(Mathf.PI * 1.5f);
            dirsPressed[2] = false;
            dirsPressed[3] = true;
        } else if (vAxis < 0.3 && vAxis > -0.3) {
            // Vertical axis is inside of the deadzone
            dirsPressed[2] = false;
            dirsPressed[3] = false;
        }
        if (controller.GetButtonDown("LeftHand") && player.GetComponent<Player>().lHand == null)
        {
            if (selectedObject.name == "UncutLettuce") {
                GameObject copy = Instantiate(lettucePrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else if (selectedObject.name == "UncutPotato") {
                GameObject copy = Instantiate(potatoPrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else if (selectedObject.name == "UncutTomato") {
                GameObject copy = Instantiate(tomatoPrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else if (selectedObject.name == "UncookedPatty") {
                GameObject copy = Instantiate(pattyPrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else if (selectedObject.name == "TopBunPrefab") {
                GameObject copy = Instantiate(topBunPrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else if (selectedObject.name == "BottomBunPrefab") {
                GameObject copy = Instantiate(btmBunPrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            } else {
                GameObject copy = Instantiate(cheesePrefab);
                player.GetComponent<Player>().pickUp(copy, true);
            }

        } else if (controller.GetButtonDown("RightHand") && player.GetComponent<Player>().rHand == null) {
            if (selectedObject.name == "UncutLettuce") {
                GameObject copy = Instantiate(lettucePrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else if (selectedObject.name == "UncutPotato") {
                GameObject copy = Instantiate(potatoPrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else if (selectedObject.name == "UncutTomato") {
                GameObject copy = Instantiate(tomatoPrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else if (selectedObject.name == "UncookedPatty") {
                GameObject copy = Instantiate(pattyPrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else if (selectedObject.name == "TopBunPrefab") {
                GameObject copy = Instantiate(topBunPrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else if (selectedObject.name == "BottomBunPrefab") {
                GameObject copy = Instantiate(btmBunPrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            } else {
                GameObject copy = Instantiate(cheesePrefab);
                player.GetComponent<Player>().pickUp(copy, false);
            }
        }
    }

    public override void complete() {
        foreach (Transform i in fridgeContainer.transform) {
            // just pick the first object in the list
            SelectObject(i.gameObject);
            break;
        }
    }

    private void MoveCursor(float angle) {
        if (selectedObject == null) {
            foreach (Transform i in fridgeContainer.transform) {
                // just pick the first object in the list
                SelectObject(i.gameObject);
                return;
            }
        }
        float squareDist = -1; // Start value is impossible
        GameObject min = null; // The index of the object closest to the cursor

        // The threshhold the dot product has to be above to be considered in the direction
        // Set to 1 / sqrt(2), or cos(45)

        float threshhold = 0.70710678118f;

        Vector3 dirVec = new Vector3(0, Mathf.Sin(angle), Mathf.Cos(angle)); // The unit vector of the angle

        foreach (Transform i in fridgeContainer.transform) {
            // Currently only considers x and y for deciding which object to go to
            // A more flexible implementation might involve projecting the line to the camera's plane and using that angle
            Vector3 ingPos = i.transform.localPosition;
            ingPos -= selectedObject.transform.localPosition;
            ingPos.x = 0;

            if (Vector3.Dot(dirVec, ingPos.normalized) >= threshhold) {
                if (squareDist < 0 || ingPos.sqrMagnitude < squareDist) {
                    squareDist = ingPos.sqrMagnitude;
                    min = i.gameObject;
                }
            }
        }
        if (min != null) {
            SelectObject(min);
        }
    }

    private void SelectObject(GameObject obj) {
        if (selectedObject == obj) {
            return;
        }
        if (selectedObject != null) {
            selectedObject.transform.localPosition -= new Vector3(0.4f, 0, 0);
        }
        obj.transform.localPosition += new Vector3(0.4f, 0, 0);

        selectedObject = obj;
    }
}