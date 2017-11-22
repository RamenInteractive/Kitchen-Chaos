using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodComponent : Item {
    private IFood _food = null;

    public IFood food {
        get {
            return _food;
        }
        set {
            _food = food;
        }
    }
}
