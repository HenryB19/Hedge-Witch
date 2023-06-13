using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject ingredientFNPrefab;
    public bool spawnerActive;
    float timer = 0;
    float timerMax = 1;
    // Update is called once per frame
    void Update()
    {
        if (spawnerActive)
        {
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                timer = 0;
                timerMax = 0.5f + UnityEngine.Random.value;
                Instantiate(ingredientFNPrefab, RandomPointInBounds(this.GetComponent<BoxCollider>().bounds), RandomRotation());
            }
        }
    }

    /// <summary>
    /// This function takes the bounds of a BoxCollider (BoxCollider.bounds) and returns a random point inside those bounds.
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// This function returns a quaternion with randomized values.
    /// </summary>
    /// <returns></returns>
    private Quaternion RandomRotation()
    {
        Quaternion rotation = Quaternion.Euler(Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f), Random.Range(0.0f, 360.0f));

        return rotation;
    }
}
