using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    public XRInteractionManager xrim;
    public Transform Shelf;
    

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject prefab  = Instantiate(PrefabToInstantiate, transform.position, transform.rotation);
        XRSimpleInteractable xrsi;
        if (prefab.TryGetComponent(out xrsi))
        {
            xrim.SelectEnter(args.interactorObject, xrsi);
            Shelf.GetComponent<ShelfData>().ingredientList[(int)prefab.GetComponent<Ingredient>().type]--;
        }
        else
        {
            Debug.Log("Shelf ingredient's prefab to instantiate dose not contain a XRSimpleInteractable.");
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
