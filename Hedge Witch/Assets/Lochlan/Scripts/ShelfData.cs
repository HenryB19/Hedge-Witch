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

    bool beingHovered;
    int hoveredItem;
    string hoveredItemLabel;

    public void IsHovering(int type)
    {
        beingHovered = true;
        hoveredItem = type;
        
        switch (hoveredItem)
        {
            case 1:
                hoveredItemLabel = "Bat Wing";
                break;
            case 2:
                hoveredItemLabel = "Snake Tongue";
                break;
            case 3:
                hoveredItemLabel = "Dog Heart";
                break;
            case 4:
                hoveredItemLabel = "Rose Thorn";
                break;
            case 5:
                hoveredItemLabel = "Midnight Mushroom";
                break;
            case 6:
                hoveredItemLabel = "Unicorn Blood";
                break;
            case 7:
                hoveredItemLabel = "Angel Tears";
                break;
            case 8:
                hoveredItemLabel = "Demon Flesh";
                break;
            default:
                hoveredItemLabel = "Frog Leg";
                break;
        }
    }

    public void NotHovering()
    {
        beingHovered = false;
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
        if (beingHovered)
        {
            canvasText[hoveredItem].text = hoveredItemLabel;
        }
    }
}
