using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    XRInteractionManager xrim;
    ShelfData shelf;

    private void Start()
    {
        xrim = GameObject.Find("XR Interaction Manager").GetComponent<XRInteractionManager>();
        shelf = GetComponentInParent<ShelfData>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject prefab = Instantiate(PrefabToInstantiate, transform.position, transform.rotation);
        XRSimpleInteractable xrsi;
        if (prefab.TryGetComponent(out xrsi))
        {
            xrim.SelectEnter(args.interactorObject, xrsi);
            shelf.ingredientList[(int)prefab.GetComponent<Ingredient>().type]--;
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
