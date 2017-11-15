using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Dictionary<string, string> buttons;
    protected Dictionary<string, string> axes;
    
    public bool GetButton(string name) {
        if (buttons.ContainsKey(name)) {
            return Input.GetButton(buttons[name]);
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    public bool GetButtonDown(string name) {
        if (buttons.ContainsKey(name)) {
            return Input.GetButtonDown(buttons[name]);
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    public bool GetButtonUp(string name) {
        if (buttons.ContainsKey(name)) {
            return Input.GetButtonUp(buttons[name]);
        }
        else {
            throw new System.Exception("No controller button of name \"" + name + "\" found");
        }
    }

    public float GetAxis(string name) {
        if (axes.ContainsKey(name)) {
            return Input.GetAxis(axes[name]);
        }
        else {
            throw new System.Exception("No controller axis of name \"" + name + "\" found");
        }
    }

    public float GetAxisRaw(string name) {
        if (axes.ContainsKey(name)) {
            return Input.GetAxisRaw(axes[name]);
        }
        else {
            throw new System.Exception("No controller axis of name \"" + name + "\" found");
        }
    }
}
