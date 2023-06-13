using System;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<Ingredient> ingredientsInCauldron = new List<Ingredient>();

    public List<Recipe> recipes = new List<Recipe>();

    [Serializable]
    public class Recipe
    {
        [Header("Input")]
        public Ingredient.IngredientType ingredient1;
        public Ingredient.IngredientType ingredient2;
        public Ingredient.IngredientType ingredient3;

        [Header("Output")]
        public GameObject potion;
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient;
        if (other.gameObject.TryGetComponent(out ingredient))
        {
            ingredientsInCauldron.Add(ingredient);
            Debug.Log(ingredient.type);
        }
        
    }
}
