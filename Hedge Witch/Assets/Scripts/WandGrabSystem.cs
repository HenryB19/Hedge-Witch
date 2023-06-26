using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class WandGrabSystem : MonoBehaviour, IPickupInputListener
{
    public GameObject particleEmmiter;
    bool atStart;

    public Transform wandTip;
    public Transform heldObjectAnchor;

    Rigidbody heldObjRb;
    private Vector3 heldObjVel;
    float heldObjDist = 0.0f;

    public LayerMask pickupLayer;
    public LayerMask shelfObjLayer;

    public float smoothDampStrength = 1.0f;

    bool wandIsHeld = false;

    public float pickupRange = 1.5f;

    public float maxAdjustment = 1.5f;
    public float minAdjustment = 0.1f;

    public float distanceAdjustmentSpeed = 1.0f;
    
    public Vector2 Stick { get; set; }
    public float Trigger { get; set; }

    private void Update()
    {
        if (heldObjRb == null) return;

        if (!wandIsHeld)
        {
            DropHeldObject();
            return;
        }

        const float deadzone = 0.1f;

        heldObjDist += Stick.y * distanceAdjustmentSpeed * Time.deltaTime;

        if (Stick.y > deadzone && heldObjRb != null)
        {
            // if value is greater than 0 increase the current distance.
            if (heldObjDist > maxAdjustment) heldObjDist = maxAdjustment;
        }
        else
        {
            // if value is less than 0 decrease the current distance.
            if (heldObjDist < minAdjustment) heldObjDist = minAdjustment;
        }

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

        if (heldObjDist < minAdjustment) heldObjDist = minAdjustment;
        else heldObjectAnchor.localPosition = new Vector3(0, 0, wandTip.localPosition.z + heldObjDist);

        heldObjRb.position = Vector3.SmoothDamp(heldObjRb.position, heldObjectAnchor.position, ref heldObjVel, smoothDampStrength * Time.fixedDeltaTime);
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        wandIsHeld = true;
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        wandIsHeld = false;
    }

    public void OnActivate(ActivateEventArgs args)
    {
        RaycastHit hit;
        ShelfIngredient shelfIngredient;
        if (Physics.Raycast(wandTip.position, wandTip.forward, out hit, pickupRange, pickupLayer.value))
        {
            if (hit.transform.gameObject.TryGetComponent(out heldObjRb))
            {
                HoldObject();
            }
        }
        else if(Physics.Raycast(wandTip.position, wandTip.forward, out hit, pickupRange, shelfObjLayer.value))
        {
            if (hit.transform.gameObject.TryGetComponent(out shelfIngredient))
            {
                GameObject prefab = Instantiate(shelfIngredient.PrefabToInstantiate, shelfIngredient.transform.position, shelfIngredient.transform.rotation);

                heldObjRb = prefab.GetComponent<Rigidbody>();

                HoldObject();

                shelfIngredient.RemoveFromShelf(prefab.GetComponent<Ingredient>().type);
            }
        }
    }
    public void OnDeactivate(DeactivateEventArgs args)
    {
        DropHeldObject();
    }
    public void HoldObject()
    {
        heldObjDist = Vector3.Distance(wandTip.position, heldObjRb.position);

        particleEmmiter.SetActive(true);

        heldObjRb.useGravity = false;
    }

    public void DropHeldObject()
    {
        if (heldObjRb == null) return;

        particleEmmiter.SetActive(false);

        heldObjRb.velocity = heldObjVel;
        heldObjRb.useGravity = true;
        heldObjRb = null;
    }
}
