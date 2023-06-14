using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfData : MonoBehaviour
{
    public List<int> ingredientList = new List<int>();
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            ingredientList.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
