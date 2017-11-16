using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class ControllerFactory {
    // Makes a controller from a 
    public static Controller MakeControllerFromId(int controllerID) {
        if (controllerID < -1 || controllerID > 11) {
            throw new Exception("Invalid Controller ID for MakeControllerFromId: " + controllerID);
        }
        Controller c = new Controller();
        if (controllerID < 0) {
            // Keyboard
            ConfigureKeyboardController(c);
            
        }
        else {
            // Controller, assumed PS4
            ConfigurePS4Controller(c, controllerID);
        }

        return c;
    }

    private static void ConfigureKeyboardController(Controller c) {
        c.SetAxis("MoveH", "Horizontal");
        c.SetAxis("MoveV", "Vertical");
        c.SetAxis("LookH", "Mouse X");
        c.SetAxis("LookY", "Mouse Y");
        c.SetButton("Jump", KeyCode.Space);
        c.SetButton("LeftHand", KeyCode.Mouse0);
        c.SetButton("RightHand", KeyCode.Mouse1);

        KeyCode[] acceptKeys = { KeyCode.Mouse0, KeyCode.Return };
        c.SetButton("Accept", acceptKeys);

        KeyCode[] backKeys = { KeyCode.Q, KeyCode.Escape };
        c.SetButton("Back", backKeys);
    }

    private static void ConfigurePS4Controller(Controller c, int controllerID) {
        int offset = controllerID * 20;

        KeyCode square = KeyCode.Joystick1Button0 + offset;
        KeyCode x = KeyCode.Joystick1Button1 + offset;
        KeyCode l1 = KeyCode.Joystick1Button4 + offset;
        KeyCode l2 = KeyCode.Joystick1Button6 + offset;
        KeyCode r1 = KeyCode.Joystick1Button5 + offset;
        KeyCode r2 = KeyCode.Joystick1Button7 + offset;
        KeyCode circle = KeyCode.Joystick1Button2 + offset;

        c.SetAxis("MoveH", "C" + controllerID + "_Horizontal");
        c.SetAxis("MoveV", "C" + controllerID + "_Vertical");
        c.SetAxis("LookH", "C" + controllerID + "_Horizontal2");
        c.SetAxis("LookY", "C" + controllerID + "_Vertical2");
        c.SetButton("Jump", circle);

        KeyCode[] leftHandKeys = { square, l1, l2 };
        c.SetButton("LeftHand", leftHandKeys);

        KeyCode[] rightHandKeys = { x, r1, r2 };
        c.SetButton("RightHand", rightHandKeys);

        KeyCode[] acceptKeys = { square, x, l1, l2, r1, r2 };
        c.SetButton("Accept", acceptKeys);

        c.SetButton("Back", circle);
    }
}
