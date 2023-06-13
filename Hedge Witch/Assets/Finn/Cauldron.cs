using System;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<Ingredient.IngredientType> ingredientsInCauldron = new List<Ingredient.IngredientType>();

    public List<Recipe> recipes = new List<Recipe>();

    public Transform potionSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient;
        if (other.gameObject.TryGetComponent(out ingredient))
        {
            other.gameObject.SetActive(false);
            ingredientsInCauldron.Add(ingredient.type);
            if (ingredientsInCauldron.Count == 3)
            {
                foreach (Recipe recipe in recipes)
                {
                    List<Ingredient.IngredientType> recipeIngredients = new List<Ingredient.IngredientType>();
                    recipeIngredients.Add(recipe.ingredient1Type);
                    recipeIngredients.Add(recipe.ingredient2Type);
                    recipeIngredients.Add(recipe.ingredient3Type);
                    recipeIngredients.Sort();
                    ingredientsInCauldron.Sort();
                    bool listsAreSame = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (recipeIngredients[i] == ingredientsInCauldron[i]) listsAreSame = true;
                    }
                    if (listsAreSame)
                    {
                        Instantiate(recipe.potion, potionSpawnPoint.position, potionSpawnPoint.rotation);
                    }
                }
            }
        }
        
    }
}
