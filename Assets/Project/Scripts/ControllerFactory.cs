﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class ControllerFactory {
    // Makes a controller from a detection ID
    public static Controller AddControllerToObj(GameObject obj, int controllerID) {
        if (controllerID < 0 || controllerID > 11) {
            throw new Exception("Invalid Controller ID for MakeControllerFromId: " + controllerID);
        }
        Controller c = obj.AddComponent<Controller>();
        if (controllerID == 0) {
            // Keyboard
            ConfigureKeyboardController(c);
            
        }
        else {
            string name = Input.GetJoystickNames()[controllerID - 1];
            switch (name)
            {
                case "Wireless Controller":
                    ConfigureGenericController(c, controllerID);
                    break;
                default:
                    ConfigurePS4Controller(c, controllerID);
                    break;
            }
        }

        return c;
    }

    private static void ConfigureKeyboardController(Controller c) {
        c.SetAxis("MoveH", "Horizontal");
        c.SetAxis("MoveV", "Vertical");
        c.SetAxis("LookH", "Mouse X");
        c.SetAxis("LookV", "Mouse Y");
        c.SetButton("Jump", KeyCode.Space);
        c.SetButton("LeftHand", KeyCode.Mouse0);
        c.SetButton("RightHand", KeyCode.Mouse1);

        KeyCode[] acceptKeys = { KeyCode.Mouse0, KeyCode.Return };
        c.SetButton("Accept", acceptKeys);

        KeyCode[] backKeys = { KeyCode.Q, KeyCode.Backspace };
        c.SetButton("Back", backKeys);

        c.deadZone = 0.3f;
    }

    private static void ConfigureGenericController(Controller c, int controllerID) {
        int offset = controllerID * 20;

        KeyCode square = KeyCode.JoystickButton0 + offset;
        KeyCode circle = KeyCode.JoystickButton2 + offset;
        KeyCode x = KeyCode.JoystickButton1 + offset;
        KeyCode triangle = KeyCode.JoystickButton3 + offset;
        KeyCode l1 = KeyCode.JoystickButton4 + offset;
        KeyCode l2 = KeyCode.JoystickButton6 + offset;
        KeyCode r1 = KeyCode.JoystickButton5 + offset;
        KeyCode r2 = KeyCode.JoystickButton7 + offset;

        c.SetAxis("MoveH", "C" + controllerID + "_Horizontal");
        c.SetAxis("MoveV", "C" + controllerID + "_Vertical");
        c.SetAxis("LookH", "C" + controllerID + "_Horizontal2");
        c.SetAxis("LookV", "C" + controllerID + "_Vertical2");
        c.SetButton("Jump", x);

        KeyCode[] leftHandKeys = { square, l1, l2 };
        c.SetButton("LeftHand", leftHandKeys);

        KeyCode[] rightHandKeys = { circle, r1, r2 };
        c.SetButton("RightHand", rightHandKeys);

        KeyCode[] acceptKeys = { square, circle, l1, l2, r1, r2 };
        c.SetButton("Accept", acceptKeys);

        c.SetButton("Back", triangle);

        c.deadZone = 0.3f;
    }

    private static void ConfigurePS4Controller(Controller c, int controllerID)
    {
        int offset = controllerID * 20;

        KeyCode x = KeyCode.JoystickButton0 + offset;
        KeyCode square = KeyCode.JoystickButton2 + offset;
        KeyCode circle = KeyCode.JoystickButton1 + offset;
        KeyCode triangle = KeyCode.JoystickButton3 + offset;
        KeyCode l1 = KeyCode.JoystickButton4 + offset;
        KeyCode l2 = KeyCode.JoystickButton6 + offset;
        KeyCode r1 = KeyCode.JoystickButton5 + offset;
        KeyCode r2 = KeyCode.JoystickButton7 + offset;

        c.SetAxis("MoveH", "C" + controllerID + "_Horizontal");
        c.SetAxis("MoveV", "C" + controllerID + "_Vertical");
        c.SetAxis("LookH", "PS4C" + controllerID + "_Horizontal2");
        c.SetAxis("LookV", "PS4C" + controllerID + "_Vertical2");
        c.SetButton("Jump", x);

        KeyCode[] leftHandKeys = { square, l1, l2 };
        c.SetButton("LeftHand", leftHandKeys);

        KeyCode[] rightHandKeys = { circle, r1, r2 };
        c.SetButton("RightHand", rightHandKeys);

        KeyCode[] acceptKeys = { square, circle, l1, l2, r1, r2 };
        c.SetButton("Accept", acceptKeys);

        c.SetButton("Back", triangle);

        c.deadZone = 0.3f;
    }
}
