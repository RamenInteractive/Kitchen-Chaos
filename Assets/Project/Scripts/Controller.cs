using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Dictionary<string, KeyCode[]> buttons;
    private Dictionary<string, string> axes;

    /**
     * Attaches a single KeyCode to a button name
     */
    public void SetButton(string name, KeyCode key) {
        KeyCode[] c = new KeyCode[1];
        c[0] = key;
        buttons[name] = c;
    } 

    /**
     * Attaches multiple KeyCodes to a button name
     */
    public void SetButton(string name, KeyCode[] keys) {
        buttons[name] = keys;
    }

    /**
     * Gets all of the KeyCodes attached to a button name
     * Will return an empty array if nothing is bound to it
     */
    public KeyCode[] GetButtonBindings(string name) {
        if (!buttons.ContainsKey(name)) {
            return new KeyCode[0];
        }
        return buttons[name];
    }

    /**
     * Gets the "local" IDs for a bindings attached to a button name.
     * For a keyboard, these values are same as their KeyCode valus.
     * For a gamepad. this will return a value based on the generic "JoystickButton" values that activate for all controllers
     * This is so that things like displaying buttons icons will be simpler
     */

    public KeyCode[] GetLocalButtonBindings(string name) {
        if (!buttons.ContainsKey(name)) {
            return new KeyCode[0];
        }
        KeyCode[] locCodes = new KeyCode[buttons[name].Length];
        for (int i = 0; i < buttons[name].Length; i++) {
            int joyCodeStart = (int)(KeyCode.JoystickButton0);
            if ((int)(buttons[name][i]) >= joyCodeStart) {
                locCodes[i] = (KeyCode)(joyCodeStart + ((int)(buttons[name][i]) - joyCodeStart) % 20);
            }
            else {
                locCodes[i] = buttons[name][i];
            }
        }
        return locCodes;
    }

    /**
     * Attaches a Unity axis to an axis name
     * In other words, it gets the name for the axis in Unity's own input manager
     * (as opposed to the local name used by this particular controller)
     */
    public void SetAxis(string name, string axis) {
        axes[name] = axis;
    }

    /**
     * Get the Unity axis attached to an axis name
     * Will return an empty string if nothing is bound to it
     */
    public string GetAxisBinding(string name) {
        if (!buttons.ContainsKey(name)) {
            return "";
        }
        return axes[name];
    }
    
    /**
     * Checks whether a button is being pressed.
     * Functions identically to Input.GetButton
     */
    public bool GetButton(string name) {
        if (buttons.ContainsKey(name)) {
            for (int i = 0; i < buttons[name].Length; i++) {
                if (Input.GetKey(buttons[name][i])) {
                    return true;
                }
            }
            return false;
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    /**
     * Checks whether a button has just been pressed down
     * Functions identicaly to Input.GetButtonDown
     */
    public bool GetButtonDown(string name) {
        if (buttons.ContainsKey(name)) {
            for (int i = 0; i < buttons[name].Length; i++) {
                if (Input.GetKeyDown(buttons[name][i])) {
                    return true;
                }
            }
            return false;
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    /**
     * Checks whether a button has just been released
     * Functions identically to Input.GetButtonUp
     */
    public bool GetButtonUp(string name) {
        if (buttons.ContainsKey(name)) {
            for (int i = 0; i < buttons[name].Length; i++) {
                if (Input.GetKeyUp(buttons[name][i])) {
                    return true;
                }
            }
            return false;
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    /**
     * Checks the value of an axis, with smoothing applied
     * Functions identically to Input.GetAxis
     */
    public float GetAxis(string name) {
        if (axes.ContainsKey(name)) {
            return Input.GetAxis(axes[name]);
        }
        else {
            throw new System.Exception("No controller axis of name \"" + name + "\" found");
        }
    }

    /**
     * Check the raw value of the axis (no smoothing)
     * Functions identically to Input.GetAxisRaw
     */
    public float GetAxisRaw(string name) {
        if (axes.ContainsKey(name)) {
            return Input.GetAxisRaw(axes[name]);
        }
        else {
            throw new System.Exception("No controller axis of name \"" + name + "\" found");
        }
    }
}
