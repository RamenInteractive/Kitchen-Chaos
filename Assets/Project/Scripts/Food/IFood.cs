using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFood
{
    bool Compare(GameObject food);

    string ingredientTicketList();
}
