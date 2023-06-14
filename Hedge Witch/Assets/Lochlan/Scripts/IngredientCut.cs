using UnityEngine;
using Random = UnityEngine.Random;

//I've found that it would be simpler to access this with a collider and strip the information to put on the shelf to there
public class IngredientCut : MonoBehaviour
{
    public int knifeCutMask;
    ShelfData shelfData;
    public int ingredientType; // this should be dependent on the individual prefab.
    public GameObject fruitHalf1;
    public GameObject fruitHalf2;
    public float ingredientTerminationHeight;
    bool cut = false;
    //public GameObject ShelfScript;
    //Remove this Camera when adding the controllers to the mechanics this is purelf for testing.
    public float cutForceScale;
    public float fruitLaunchVelocity;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        shelfData = GameObject.Find("Shelf").GetComponent<ShelfData>();
        //this.GetComponent<Rigidbody>().velocity = new Vector3(0,fruitLaunchVelocity,0);
        //this.GetComponent <Rigidbody>().angularVelocity = new Vector3(0,fruitLaunchVelocity,0);

        // when we have prefab variant objects for

    }
    
    // Update is called once per frame
    void Update()
    {
     
        if (Input.GetMouseButtonDown(0) && !cut)
        {

            this.SplitIngredient();    
        }

        if(this.transform.position.y < ingredientTerminationHeight)
        {
            Destroy(fruitHalf1);
            Destroy(fruitHalf2);
            Destroy(this.gameObject);
        }
    }

    void SplitIngredient()
    {
        cut = true;
        float randomX = Random.value * 2 - 1;
        float randomY = Random.value * 2 - 1;
        float randomZ = Random.value * 2 - 1;
        Rigidbody half1 = fruitHalf1.GetComponent<Rigidbody>();
        Rigidbody half2 = fruitHalf2.GetComponent<Rigidbody>();
        fruitHalf1.GetComponent<BoxCollider>().enabled = true;
        fruitHalf2.GetComponent<BoxCollider>().enabled = true;

        this.GetComponent<Rigidbody>().isKinematic = true;

        half1.isKinematic = false;
        half2.isKinematic = false;

        // so let's run through the math for this
        // I need to get a direction to apply the force in respect to the other half of the ingredient so I first need a unit vector
        // between the two halves
        Vector3 forceDirection = Vector3.Normalize(fruitHalf1.transform.position - fruitHalf2.transform.position);
        Vector3 randomVariation = new Vector3(randomX, randomY, randomZ) * 0.1f;
        // then scale the diretion based on the amount of force we want to apply
        // put the finalized force scale here maybe as a random value
        // with that done now we need to know where to apply the force, 
        half1.AddForceAtPosition(forceDirection + randomVariation * cutForceScale, fruitHalf1.transform.position);
        half2.AddForceAtPosition(-forceDirection + -randomVariation * cutForceScale, fruitHalf2.transform.position);


        shelfData.ingredientList[ingredientType] += 1;

        Destroy(fruitHalf1, 2.5f);
        Destroy(fruitHalf2, 2.5f);
        Destroy(this.gameObject, 2.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == knifeCutMask)
        {
            this.SplitIngredient();
        }
    }
}
