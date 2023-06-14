using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class GrabSystem : MonoBehaviour
{
    XRRayInteractor rayInteractor;
    SpringJoint springJoint;
    Rigidbody thisRB;

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
    }
    private void Update()
    {
        if (!springJoint.connectedBody) return;

        currentObjDistance += input.ReadValue<Vector2>().y * distanceAdjustmentSpeed * Time.deltaTime;
        springJoint.anchor = new Vector3(0, 0, currentObjDistance);
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
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {

    }


    public void OnHoverExited(HoverExitEventArgs args)
    {

    }
}
