using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeGame : Minigame
{
    // directions pressed. Order: up, down, left, right
    private bool[] dirsPressed;

    private Vector3 cursor;

    // The currently selected object within the object list
    private int selectedIndex = 0;

    new protected void Start() {
        base.Start();
        dirsPressed = new bool[4];
    }

    new protected void Update() {
        base.Update();
        if (InUse) {
            // Input checking: Check the stick/keyboard/d-pad for input
            // May later be replaced by a more robust input system

            // Check the horizontal axis direction
            if (Input.GetAxis("Horizontal") > 0.3) {
                moveCursor(0);
                dirsPressed[0] = true;
                dirsPressed[1] = false;
            }
            else if (Input.GetAxis("Horizontal") < 0.3) {
                moveCursor(Mathf.PI);
                dirsPressed[1] = true;
                dirsPressed[0] = false;
            }
            else {
                // Horizontal axis is inside of the deadzone
                dirsPressed[0] = false;
                dirsPressed[0] = false;
            }

            // Check the vertical axis direction
            if (Input.GetAxis("Vertical") > 0.3) {
                moveCursor(Mathf.PI * 0.5f);
                dirsPressed[2] = true;
                dirsPressed[3] = false;
            }
            else if (Input.GetAxis("Vertical") < 0.3) {
                moveCursor(Mathf.PI * 1.5f);
                dirsPressed[3] = true;
                dirsPressed[2] = false;
            }
            else {
                // Vertical axis is inside of the deadzone
                dirsPressed[2] = false;
                dirsPressed[3] = false;
            }
        }
    }

    public override void complete() {
    }

    private void moveCursor(float angle) {
        if (ingredients.Length > 0) {
            List<Ingredient> ingList = ingredients[0];
            float squareDist = -1; // Start value is impossible
            int minIndex = -1; // The index of the object closest to the cursor

            // The threshhold the dot product has to be above to be considered in the direction
            // Set to the dot product for 2 unit vectors separated by a 45 degree angle
            float threshhold = Vector2.Dot(new Vector2(1 / Mathf.Sqrt(2), 1 / Mathf.Sqrt(2)), new Vector2(0, 1));

            Vector3 dirVec = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0); // The unit vector of the angle

            foreach (Ingredient i in ingList) {
                // Currently only considers x and y for deciding which object to go to
                // A more flexible implementation might involve projecting the line to the camera's plane and using that angle
                Vector3 ingPos = i.transform.position;
                ingPos -= transform.position;
                ingPos.z = 0;

                if (Vector3.Dot(dirVec, ingPos.normalized) >= threshhold) {
                    if (squareDist < 0 || squareDist > ingPos.sqrMagnitude) {
                        squareDist = ingPos.sqrMagnitude;
                        minIndex = ingList.IndexOf(i);
                    }
                }

            }
        }
    }
}