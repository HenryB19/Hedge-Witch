using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Ingredient;

public class ShelfData : MonoBehaviour
{
    public List<int> ingredientList = new List<int>();
    public GameObject canvas;
    public List<TextMeshProUGUI> canvasText;
    public List<Transform> ingredientContainers;

    void Awake()
    {
        for (int i = 0; i < 9; i++)
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

        // GetChild 1 should correspond to the child object of Shelf, Ingredients. 
        Transform shelfIngredients = transform.GetChild(1); ;
        for (int i = 0; i < shelfIngredients.transform.childCount; i++)
        {
            ingredientContainers.Add(shelfIngredients.transform.GetChild(i));
        }

    }

    private void Update()
    {
        // this will cause issues if canvasText count is different from ingredientContainers count.
        for (int i = 0; i < canvasText.Count; i++)
        {
            
            if (ingredientList[i] <= 0)
            {
                ingredientContainers[i].gameObject.SetActive(false);
                canvasText[i].gameObject.SetActive(false);
            }
            else
            {
                ingredientContainers[i].gameObject.SetActive(true);
                canvasText[i].gameObject.SetActive(true);
                canvasText[i].text = ingredientList[i].ToString();
            }
        }


    }
}
