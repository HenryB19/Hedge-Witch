using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupInputListener
{
    float Trigger { set; }
    Vector2 Stick { set; }
}
