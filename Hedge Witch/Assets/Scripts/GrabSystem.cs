using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabSystem : MonoBehaviour
{
    XRRayInteractor rayInteractor;

    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Transform otherTf = args.interactableObject.transform;
        rayInteractor.attachTransform = otherTf;

    }

    public void OnSelectExited(SelectExitEventArgs args)
    {

    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {

    }


    public void OnHoverExited(HoverExitEventArgs args)
    {

    }
}
