using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InputManager : MonoBehaviour
{
    public Transform rightHand;
    public Transform leftHand;

    public Transform wand;
    public Transform knife;

    public XRRayInteractor rightXRRayInteractor;
    public XRRayInteractor leftXRRayInteractor;

    bool wandInRightHand = true;

    // Start is called before the first frame update
    void Start()
    {
        leftXRRayInteractor.enabled = false;
        wand.parent = rightHand;
        knife.parent = leftHand;
    }

    public void SwapHands()
    {
        if (wandInRightHand)
        {
            rightXRRayInteractor.enabled = false;
            leftXRRayInteractor.enabled = true;
            wand.parent = leftHand;
            knife.parent = rightHand;
            wandInRightHand = false;
        }
        else
        {
            rightXRRayInteractor.enabled = true;
            leftXRRayInteractor.enabled = false;
            wand.parent = rightHand;
            knife.parent = leftHand;
            wandInRightHand = true;
        }
        wand.localRotation = Quaternion.identity;
        knife.localRotation = Quaternion.identity;
        wand.localPosition = Vector3.zero;
        knife.localPosition = Vector3.zero;
    }
}
