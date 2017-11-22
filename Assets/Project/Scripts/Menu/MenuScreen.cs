using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenuScreen : MonoBehaviour {
    private float deadZone = 0.3f;

    protected float GetAnyAxis(string axisName) {
        float rAxis = 0;
        for (int i = 0; i < 9; i++) {
            if (i == 0) { // Keyboard
                rAxis = Input.GetAxis(axisName);
            }
            else {
                rAxis = Input.GetAxis("C" + i + "_" + axisName);
            }
            if (rAxis > deadZone || rAxis < -deadZone) {
                break;
            }
        }
        return rAxis;
    }

    protected bool GetAnyAccept() {
        return Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.JoystickButton1);
    }

    protected bool GetAnyStart() {
        return Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton9);
    }

    protected bool GetAnyBack() {
        return Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton2);
    }
}
