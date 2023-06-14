using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelfData : MonoBehaviour
{
    public List<int> ingredientList = new List<int>();
    public GameObject canvas;
    public List<TextMeshProUGUI> canvasText;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            ingredientList.Add(0);
        }
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>().gameObject;
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            canvasText.Add(GetComponentsInChildren<TextMeshProUGUI>()[i]);
        }

    }

    private void Update()
    {
        foreach (int i in ingredientList)
        {
            canvasText[i].text = ingredientList[i].ToString();
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
