using System;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    private List<Ingredient.IngredientType> ingredientsInCauldron = new List<Ingredient.IngredientType>();

    public List<Recipe> recipes = new List<Recipe>();

    public Transform lovePotionSpawnPoint;
    public Transform sleepPotionSpawnPoint;
    public Transform truthPotionSpawnPoint;

    public AudioSource splashSource;
    public AudioSource ingredientSoundSource;

    public ParticleSystem potionParticle;
    public ParticleSystem failParticle;
    public ParticleSystem splashParticle;


    private void OnTriggerEnter(Collider other)
    {
        splashParticle.Play();
        Ingredient ingredient;
        if (other.gameObject.TryGetComponent(out ingredient))
        {
            ingredientSoundSource.clip = ingredient.droppedInCauldronSound; 
            ingredientSoundSource.Play();
            splashSource.Play();

            other.gameObject.SetActive(false); // CHANGE TO DESTROY
            ingredientsInCauldron.Add(ingredient.type);
            if (ingredientsInCauldron.Count == 3)
            {
                int z = 0;
                foreach (Recipe recipe in recipes)
                {
                    List<Ingredient.IngredientType> recipeIngredients = new List<Ingredient.IngredientType>();
                    recipeIngredients.Add(recipe.ingredient1Type);
                    recipeIngredients.Add(recipe.ingredient2Type);
                    recipeIngredients.Add(recipe.ingredient3Type);
                    recipeIngredients.Sort();
                    ingredientsInCauldron.Sort();

                    bool listsAreSame = false;
                    for (int i = 0; i < ingredientsInCauldron.Count; i++)
                    {
                        if (recipeIngredients[i] == ingredientsInCauldron[i])
                        {
                            listsAreSame = true;
                            potionParticle.Play();
                        }
                        else
                        {
                            listsAreSame = false;
                            failParticle.Play();
                        }
                    }
                    
                    if (!listsAreSame)
                    {
                       
                        z++;
                        continue;
                    }

                    Transform thisPos = lovePotionSpawnPoint;
                    if (z == 1) thisPos = sleepPotionSpawnPoint;
                    else if (z == 2) thisPos = truthPotionSpawnPoint;
                    Instantiate(recipe.potion, thisPos.position, thisPos.rotation);
                }
                ingredientsInCauldron = new List<Ingredient.IngredientType>();
            }
        }
    }
}
