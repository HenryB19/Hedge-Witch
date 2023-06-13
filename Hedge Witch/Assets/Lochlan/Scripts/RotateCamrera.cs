using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotateCamrera : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.transform.Rotate(-20, 0, 0);
            this.transform.Rotate(0, -90, 0);
            this.transform.Rotate(20, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            this.transform.Rotate(-20, 0, 0);
            this.transform.Rotate(0, 90, 0);
            this.transform.Rotate(20, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            this.transform.Rotate(-20, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            this.transform.Rotate(20, 0, 0);
        }
    }
}
