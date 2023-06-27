using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Cauldron : MonoBehaviour
{
    private List<Ingredient.IngredientType> ingredientsInCauldron = new List<Ingredient.IngredientType>();

    public List<Recipe> recipes = new List<Recipe>();

    public Transform lovePotionSpawnPoint;
    public Transform sleepPotionSpawnPoint;
    public Transform truthPotionSpawnPoint;

    public AudioSource splashSource;
    public AudioSource ingredientSoundSource;
    public AudioSource successSource;
    public AudioSource failureSource;


    public ParticleSystem potionParticle;
    public ParticleSystem failParticle;
    public ParticleSystem splashParticle;


    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient;
        if (other.gameObject.TryGetComponent(out ingredient))
        {
            ingredientSoundSource.clip = ingredient.droppedInCauldronSound; 
            ingredientSoundSource.Play();
            splashParticle.Play();
            splashSource.Play();

            other.gameObject.SetActive(false); // CHANGE TO DESTROY
            ingredientsInCauldron.Add(ingredient.type);
            if (ingredientsInCauldron.Count == 3)
            {
                for (int r = 0; r < 3; r++)
                {
                    List<Ingredient.IngredientType> recipeIngredients = new List<Ingredient.IngredientType>();
                    recipeIngredients.Add(recipes[r].ingredient1Type);
                    recipeIngredients.Add(recipes[r].ingredient2Type);
                    recipeIngredients.Add(recipes[r].ingredient3Type);
                    recipeIngredients.Sort();
                    ingredientsInCauldron.Sort();

                    if (
                        recipeIngredients[0] == ingredientsInCauldron[0] &&
                        recipeIngredients[1] == ingredientsInCauldron[1] &&
                        recipeIngredients[2] == ingredientsInCauldron[2]
                        )
                    {
                        Transform thisPos = lovePotionSpawnPoint;
                        switch (r)
                        {
                            case 1:
                                thisPos = sleepPotionSpawnPoint;
                                break;
                            case 2:
                                thisPos = truthPotionSpawnPoint;
                                break;
                        }
                        Instantiate(recipes[r].potion, thisPos.position, thisPos.rotation);
                        potionParticle.Play();
                        successSource.Play();

                        ingredientsInCauldron = new List<Ingredient.IngredientType>();
                        return;
                    }
                }
                failParticle.Play();
                failureSource.Play();
                ingredientsInCauldron = new List<Ingredient.IngredientType>();
            }
        }
        else
        {
            Vector3 dir = new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1), Random.Range(-1, 1));
            other.attachedRigidbody.AddForce(dir * 10, ForceMode.Impulse);
        }
    }
}
