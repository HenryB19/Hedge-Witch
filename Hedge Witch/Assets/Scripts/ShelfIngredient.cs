using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShelfIngredient : MonoBehaviour
{
    public GameObject PrefabToInstantiate;
    public XRInteractionManager xrim;

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject prefab  = Instantiate(PrefabToInstantiate);
        XRSimpleInteractable xrsi;
        if (prefab.TryGetComponent(out xrsi))
        {
            xrim.ForceSelect(args.interactorObject, xrsi);
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
