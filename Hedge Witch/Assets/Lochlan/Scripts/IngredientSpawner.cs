using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{

    public GameObject[] ingredientFNPrefab;
    public bool spawnerActive;
    public bool spawnTimeRandomVal0To1;
    public float minSpawnTime = 0.5f;
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
                if (spawnTimeRandomVal0To1) // if random active add a random amount of time between zero and 1 second to spawn time
                    timerMax = minSpawnTime + UnityEngine.Random.value;
                else 
                    timerMax = minSpawnTime;
                Instantiate(ingredientFNPrefab[(int)math.round(UnityEngine.Random.Range(-0.49f, 8.49f))], RandomPointInBounds(this.GetComponent<BoxCollider>().bounds), RandomRotation());
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
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    /// <summary>
    /// This function returns a quaternion with randomized values.
    /// </summary>
    /// <returns></returns>
    private Quaternion RandomRotation()
    {
        Quaternion rotation = Quaternion.Euler(
            UnityEngine.Random.Range(0.0f, 360.0f),
            UnityEngine.Random.Range(0.0f, 360.0f),
            UnityEngine.Random.Range(0.0f, 360.0f)
        );

        return rotation;
    }

    void SetActive(bool Active)
    {
        spawnerActive = Active;
    }

    bool GetActive()
    {
        return spawnerActive;
    }
}
