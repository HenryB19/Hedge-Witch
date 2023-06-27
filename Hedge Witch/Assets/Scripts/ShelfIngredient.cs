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

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject obj = Instantiate(PrefabToInstantiate, transform.position, transform.rotation);
        args.interactorObject.IsSelecting(obj.GetComponent<XRSimpleInteractable>());
        RemoveFromShelf(obj.GetComponent<Ingredient>().type);
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        
    }
}
