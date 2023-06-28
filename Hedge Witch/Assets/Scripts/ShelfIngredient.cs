using UnityEngine;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;

    ShelfData shelf;

    private void Start()
    {
        shelf = GetComponentInParent<ShelfData>();
    }

    public GameObject TakeObject()
    {
        GameObject obj = Instantiate(PrefabToInstantiate, transform.position, transform.rotation);
        shelf.ingredientList[(int)PrefabToInstantiate.GetComponent<Ingredient>().type]--;
        return obj;
    }
}
