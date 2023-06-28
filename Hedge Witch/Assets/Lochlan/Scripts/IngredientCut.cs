using UnityEngine;
using Random = UnityEngine.Random;

//I've found that it would be simpler to access this with a collider and strip the information to put on the shelf to there

public class IngredientCut : MonoBehaviour
{
    public int knifeCutMask;
    ShelfData shelfData;
    public int ingredientType; // this should be dependent on the individual prefab.
    public GameObject ingredientHalf1;
    public GameObject ingredientHalf2;
    public GameObject ingredientWhole;
    public AudioSource cutSound;
    public float ingredientTerminationHeight;
    bool cut = false;
    public float cutForceScale;
    public float fruitLaunchVelocity;

    void Start()
    {
        // I believe there should be a way to pass in a reference when instantiating an object should in theory be better than running Find.
        shelfData = GameObject.Find("Shelf").GetComponent<ShelfData>();
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            this.SplitIngredient();
        }

        if (this.transform.position.y < ingredientTerminationHeight)
        {
            Destroy(ingredientHalf1);
            Destroy(ingredientHalf2);
            Destroy(this.gameObject);
        }
    }

    void SplitIngredient()
    {
        if (!cut)
        {
            cut = true;
            float randomX = Random.value * 2 - 1;
            float randomY = Random.value * 2 - 1;
            float randomZ = Random.value * 2 - 1;
            Rigidbody half1 = ingredientHalf1.GetComponent<Rigidbody>();
            Rigidbody half2 = ingredientHalf2.GetComponent<Rigidbody>();
            ingredientHalf1.SetActive(true);
            ingredientHalf2.SetActive(true);
            ingredientWhole.SetActive(false);

            this.GetComponent<Rigidbody>().isKinematic = true;
            this.GetComponent<BoxCollider>().enabled = false;

            this.GetComponent<AudioSource>().Play();

            // so let's run through the math for this
            // I need to get a direction to apply the force in respect to the other half of the ingredient so I first need a unit vector
            // between the two halves
            Vector3 forceDirection = Vector3.Normalize(ingredientHalf1.transform.position - ingredientHalf2.transform.position);
            Vector3 randomVariation = new Vector3(randomX, randomY, randomZ) * 0.1f;
            // then scale the diretion based on the amount of force we want to apply
            // put the finalized force scale here maybe as a random value
            // with that done now we need to know where to apply the force,
            
            half1.AddForceAtPosition((forceDirection + randomVariation) * cutForceScale, ingredientHalf1.transform.position);
            half2.AddForceAtPosition((-forceDirection + -randomVariation) * cutForceScale, ingredientHalf2.transform.position);

            shelfData.ingredientList[ingredientType] += 1;

            cutSound.Play();

            Destroy(ingredientHalf1, 2.5f);
            Destroy(ingredientHalf2, 2.5f);
            Destroy(this.gameObject, 2.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == knifeCutMask)
        {
            this.SplitIngredient();
        }
    }
}
