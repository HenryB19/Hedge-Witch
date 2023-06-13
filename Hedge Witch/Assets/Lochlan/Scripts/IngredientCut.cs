using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientCut : MonoBehaviour
{
    int ingredientType;
    public GameObject fruitHalf1;
    public GameObject fruitHalf2;
    //Remove this Camera when adding the controllers to the mechanics this is purelf for testing.
    Camera cam;
    public float forceScale;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        ingredientType = math.round(Random.value * 10) ;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {

            this.SplitIngredient();
        }
    }

    void SplitIngredient()
    {
        float randomX = Random.value * 2 - 1;
        float randomY = Random.value * 2 - 1;
        float randomZ = Random.value * 2 - 1;
        Rigidbody half1 = fruitHalf1.GetComponent<Rigidbody>();
        Rigidbody half2 = fruitHalf2.GetComponent<Rigidbody>();

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
        half1.AddForceAtPosition(forceDirection + randomVariation * forceScale, fruitHalf1.transform.position);
        half2.AddForceAtPosition(-forceDirection + -randomVariation * forceScale, fruitHalf2.transform.position);
    }
}
