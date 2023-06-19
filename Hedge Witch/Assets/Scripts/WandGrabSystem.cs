using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System;
using UnityEditor;

public class WandGrabSystem : MonoBehaviour
{

    XRRayInteractor rayInteractor;
    SpringJoint springJoint;
    Rigidbody thisRB;

    public Transform wandTip;
    public GameObject particleEmmiter;
    bool atStart;

    Rigidbody heldObjectRb;
    float pickupDrag;
    float pickupAngularDrag;

    float currentObjDistance = 0.0f;
    Transform currentObjTransform;

    public int pickupLayer;
    public float pickupRange = 2.0f;

    public float maxAdjustment = 1.5f;
    public float minAdjustment = 0.5f;
    public float distanceAdjustmentSpeed = 1.0f;

    public InputAction joystickLeftInput;
    public InputAction triggeLeftInput;
    public InputAction joystickRightInput;
    public InputAction triggerRightInput;

    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        springJoint = GetComponent<SpringJoint>();
        springJoint.anchor = wandTip.transform.localPosition;
        thisRB = GetComponent<Rigidbody>();

        joystickLeftInput.Enable();
        triggeLeftInput.Enable();
        joystickRightInput.Enable();
        triggerRightInput.Enable();

        particleEmmiter.SetActive(false);
    }
    private void Update()
    {
        if (!springJoint.connectedBody) return;

        HandleInput(joystickRightInput.ReadValue<Vector2>(), triggerRightInput.ReadValue<float>());

        if (currentObjDistance < minAdjustment) currentObjDistance = minAdjustment;
        else springJoint.anchor = new Vector3(0, 0, currentObjDistance);

        if (atStart)
        {
            particleEmmiter.transform.position = heldObjectRb.position;
            atStart = false;
        }
        else
        {
            particleEmmiter.transform.position = wandTip.position;
            atStart = true;
        }

       
    }

    public void HandleInput(Vector2 joystick, float trigger)
    {
        if (joystick.y > 0)
        {
            // if value is greater than 0 increase the current distance.
            if (currentObjDistance > maxAdjustment) currentObjDistance = maxAdjustment;
            else currentObjDistance += joystick.y * distanceAdjustmentSpeed * Time.deltaTime;
        }
        else
        {
            // if value is less than 0 decrease the current distance.
            if (currentObjDistance < minAdjustment) currentObjDistance = minAdjustment;
            else currentObjDistance += joystick.y * distanceAdjustmentSpeed * Time.deltaTime;
        }

        if (trigger > 0 && heldObjectRb == null)
        {
            // if the trigger is being pressed and we arent allready holding an object cast a ray from the wandTip.
            RaycastHit hit;
            if (!Physics.Raycast(wandTip.position, wandTip.forward, out hit, pickupRange, pickupLayer)) return; // if ray dosent hit exit early.
            if (hit.transform.TryGetComponent(out heldObjectRb))
            {
                currentObjDistance = Vector3.Distance(wandTip.position, hit.transform.position);

                // store current rigidbody values so we can have different values for held objects.
                pickupDrag = heldObjectRb.drag;
                pickupAngularDrag = heldObjectRb.angularDrag;

                // set their held values.
                heldObjectRb.drag = thisRB.drag;
                heldObjectRb.angularDrag = thisRB.angularDrag;
                heldObjectRb.useGravity = false;

                // connect spring joint.
                springJoint.connectedBody = heldObjectRb;
                springJoint.anchor = new Vector3(0, 0, currentObjDistance);

                particleEmmiter.SetActive(true);
            }
        }
        else if (trigger == 0 && heldObjectRb != null)
        {
            // if the trigger isnt being pressed and we are holding an object we drop the object.

            // set the objects old rigidbody values and sets the heldObjectRb to null.
            heldObjectRb.drag = pickupDrag;
            heldObjectRb.angularDrag = pickupAngularDrag;
            heldObjectRb.useGravity = true;
            heldObjectRb = null;

            // disconnect springjoint.
            springJoint.connectedBody = null;

            particleEmmiter.SetActive(false);
        }
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        //if (args.interactorObject.)
    }
}
