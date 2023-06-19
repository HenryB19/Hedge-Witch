using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    public XRInteractionManager xrim;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject prefab  = Instantiate(PrefabToInstantiate, transform.position, transform.rotation);
        XRSimpleInteractable xrsi;
        if (prefab.TryGetComponent(out xrsi))
        {
            xrim.SelectEnter(args.interactorObject, xrsi);
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
