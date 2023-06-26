using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
  
    ShelfData shelf;

    private void Start()
    {
        shelf = GetComponentInParent<ShelfData>();
    }

    public void RemoveFromShelf(Ingredient.IngredientType type)
    {
        shelf.ingredientList[(int)type]--;
    }
}
