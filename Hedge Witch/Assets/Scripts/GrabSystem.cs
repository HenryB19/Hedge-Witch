using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GrabSystem : MonoBehaviour
{
    XRRayInteractor rayInteractor;
    SpringJoint springJoint;
    Rigidbody thisRB;

    public Transform wandTip;
    public GameObject particleEmmiter;
    bool atStart;

    Rigidbody pickupRB;
    float pickupDrag;
    float pickupAngularDrag;

    float currentObjDistance = 0.0f;
    Transform currentObjTransform;

    public float distanceAdjustmentSpeed = 1.0f;
    public InputAction input;

    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        springJoint = GetComponent<SpringJoint>();
        thisRB = GetComponent<Rigidbody>();
        input.Enable();
        particleEmmiter.SetActive(false);
    }
    private void Update()
    {
        if (!springJoint.connectedBody) return;

        currentObjDistance += input.ReadValue<Vector2>().y * distanceAdjustmentSpeed * Time.deltaTime;
        springJoint.anchor = new Vector3(0, 0, currentObjDistance);

        if (atStart)
        {
            particleEmmiter.transform.position = pickupRB.position;
            atStart = false;
        }
        else
        {
            particleEmmiter.transform.position = wandTip.position;
            atStart = true;
        }
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Vector3 handPos = rayInteractor.transform.position;
        Vector3 pickupPos = args.interactableObject.transform.position;

        currentObjDistance = Vector3.Distance(handPos, pickupPos);

        if (args.interactableObject.transform.gameObject.TryGetComponent(out pickupRB))
        {
            pickupDrag = pickupRB.drag;
            pickupAngularDrag = pickupRB.angularDrag;

            pickupRB.drag = thisRB.drag;
            pickupRB.angularDrag = thisRB.angularDrag;
            pickupRB.useGravity = false;

            springJoint.connectedBody = pickupRB;
            springJoint.anchor = new Vector3(0, 0, currentObjDistance);

            particleEmmiter.SetActive(true);
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (!pickupRB) return;

        pickupRB.drag = pickupDrag;
        pickupRB.angularDrag = pickupAngularDrag;
        pickupRB.useGravity = true;
        pickupRB = null;

        springJoint.connectedBody = null;

        particleEmmiter.SetActive(false);
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {

    }


    public void OnHoverExited(HoverExitEventArgs args)
    {

    }
}
