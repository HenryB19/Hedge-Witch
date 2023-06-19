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

    /// <summary>
    /// this function removes an ingredient from the List of ingredients
    /// </summary>
    /// <param name="ingredientType"></param>
    void RemoveIngredient(int ingredientType)
    {
        ingredientList[ingredientType] -= 1;
    }

    bool CheckIngredientIsZero(int ingredientType)
    {
        return ingredientList[ingredientType] > 0;
    }


}
