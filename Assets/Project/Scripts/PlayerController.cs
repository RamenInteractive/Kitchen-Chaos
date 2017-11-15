using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PlayerController : Controller {
    public string movementUpDownAxis;
    public string movementLeftRightAxis;
    public string cameraUpDownAxis;
    public string cameraLeftRightAxis;
    public string interactButton;

    public void Start() {
        axes["movementUpDownAxis"] = movementUpDownAxis;
        axes["movementLeftRightAxis"] = movementLeftRightAxis;
        axes["cameraUpDownAxis"] = cameraUpDownAxis;
        axes["cameraLeftRightAxis"] = cameraLeftRightAxis;
        buttons["interactButton"] = interactButton;
    }
}
