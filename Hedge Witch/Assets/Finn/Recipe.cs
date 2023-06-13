using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    [Header("Input")]
    public Ingredient.IngredientType ingredient1Type;
    public Ingredient.IngredientType ingredient2Type;
    public Ingredient.IngredientType ingredient3Type;

    [Header("Output")]
    public GameObject potion;
}
