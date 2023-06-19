using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.OpenXR.Features.Interactions;
using XRController = UnityEngine.InputSystem.XR.XRController;

public sealed class VRInputPasser : MonoBehaviour
{
    [SerializeField] private Chirality chirality;

    private XRRayInteractor interactor;

    private void Awake()
    {
        interactor = GetComponent<XRRayInteractor>();
    }

    private void Update()
    {
        var controller = chirality == Chirality.Left ? XRController.leftHand : XRController.rightHand;

        var trigger = 0.0f;
        var stick = Vector2.zero;

        switch (controller)
        {
            case OculusTouchController touchController:
                trigger = touchController.trigger.ReadValue();
                stick = touchController.thumbstick.ReadValue();
                break;
            case OculusTouchControllerProfile.OculusTouchController touchController:
                trigger = touchController.trigger.ReadValue();
                stick = touchController.thumbstick.ReadValue();
                break;
        }

        foreach (var selection in interactor.interactablesSelected)
        {
            var listener = selection.transform.GetComponent<IPickupInputListener>();
            if (listener == null) continue;

            listener.Trigger = trigger;
            listener.Stick = stick;
        }

        //interactor.interactablesSelected
    }

    public enum Chirality
    {
        Left,
        Right
    }
}
