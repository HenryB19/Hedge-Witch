using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;


public class WandGrabSystem : MonoBehaviour, IPickupInputListener
{
    public GameObject particleEmmiter;
    bool atStart;

    public Transform wandTip;
    public Transform heldObjectAnchor;

    Ingredient heldIngredient;
    Rigidbody heldObjRb;
    private Vector3 heldObjVel;
    float heldObjDist = 0.0f;

    public float smoothDampStrength = 1.0f;

    public float pickupRange = 1.5f;

    public float maxAdjustment = 1.5f;
    public float minAdjustment = 0.1f;

    public float distanceAdjustmentSpeed = 1.0f;
    
    public Vector2 Stick { get; set; }
    public float Trigger { get; set; }

    XRSimpleInteractable currentHovered;

    public PlayerInput playerInput;

    private void Start()
    {
        playerInput.actions.FindActionMap("XRI RightHand Interaction").FindAction("Translate Anchor").performed +=
            callback =>
            {
                if (heldObjRb == null) return;

                Vector2 value = callback.ReadValue<Vector2>();

                heldObjDist += value.y * distanceAdjustmentSpeed * Time.deltaTime;

                if (heldObjDist > maxAdjustment) heldObjDist = maxAdjustment;
                if (heldObjDist < minAdjustment) heldObjDist = minAdjustment;
            };
    }

    private void Update()
    {
        if (heldObjRb == null) return;

        if (atStart)
        {
            particleEmmiter.transform.position = heldObjRb.position;
            atStart = false;
        }
        else
        {
            particleEmmiter.transform.position = wandTip.position;
            atStart = true;
        }
    }

    private void FixedUpdate()
    {
        if (heldObjRb == null) return;

        heldObjectAnchor.localPosition = new Vector3(0, 0, wandTip.localPosition.z + heldObjDist);

        heldObjRb.position = Vector3.SmoothDamp(heldObjRb.position, heldObjectAnchor.position, ref heldObjVel, smoothDampStrength * Time.fixedDeltaTime);
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        GameObject obj;
        ShelfIngredient si;
        if (args.interactableObject.transform.TryGetComponent(out si))
        {
            obj = si.TakeObject();
            heldIngredient = obj.GetComponent<Ingredient>();
            heldIngredient.PlaySoundOnCollision(false);
        }
        else
        {
            obj = args.interactableObject.transform.gameObject;
        }

        if (obj.TryGetComponent(out heldObjRb))
        {
            heldObjDist = Vector3.Distance(wandTip.position, heldObjRb.position);

            particleEmmiter.SetActive(true);

            heldObjRb.useGravity = false;
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        if (heldIngredient != null)
        {
            heldIngredient.PlaySoundOnCollision(true);
            heldIngredient = null;
        }
        if (heldObjRb!= null)
        {
            particleEmmiter.SetActive(false);

            heldObjRb.velocity = heldObjVel;
            heldObjRb.useGravity = true;
            heldObjRb = null;
        }
    }
}
