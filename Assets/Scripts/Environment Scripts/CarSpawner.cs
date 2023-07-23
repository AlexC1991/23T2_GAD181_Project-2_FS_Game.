using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    // Declaration of the Prefab list of cars to spawn and the end of the route object
    [SerializeField] private List<GameObject> carPrefabList;
    [SerializeField] private GameObject carRouteEndObject;

    // Declaration of an Enumerator used to repeat a method
    private IEnumerator coroutine;

    // Declaration of the cars spawn position
    private Vector3 spawnPosition;

    // Declaration of fvariables that assist in the spawning of the cars
    private float startDelay = 2f;
    private float spawnInterval;
    public float carSpawnIndex;
    private float timeToDestroy;
    private float originalTimeToDestroy;
    [HideInInspector] public static bool finishedWithCar;

    // Plays at start
    private void Start()
    {
        // Sets the spawn position to that of the object attached to this script
        spawnPosition = this.transform.position;
        //SpawnRandomCar();
        // Calls the SpawnRandomCar method with a delay of startDelay (=2)
        Invoke("SpawnRandomCar", startDelay);
        
        timeToDestroy = 1;
        originalTimeToDestroy = timeToDestroy;
        finishedWithCar = false;
    }

    // Updates every frame
    private void Update()
    {
        // Updates the car index (used for spawning the cars from the list) to a random number
        carSpawnIndex = Random.Range(0, carPrefabList.Count);

        // Updates the spawning interval to a random number
        spawnInterval = Random.Range(1.5f, 6f);
        
        //Debug.Log(timeToDestroy);
        //Debug.Log(finishedWithCar);
        //timeToDestroy -= 0.3f * Time.deltaTime;
    }

    // Method used to spawn a new car
    private void SpawnRandomCar()
    {
        // Randomizes the car chosen to spawn (Could Remove?)
        int carSpawnIndex = Random.Range(0, carPrefabList.Count);

        // Instantiates a new car prefab from the list of cars
        GameObject newCar = Instantiate(carPrefabList[carSpawnIndex], spawnPosition, Quaternion.Euler(0,90,0), this.gameObject.transform);

        // Sets up the coroutine that moves the cars down a certain route
        coroutine = MoveNewCar(newCar);
        StartCoroutine(coroutine);

        // Re-randomizes the car spawn interval and recalls this method
        spawnInterval = Random.Range(2f, 4f);
        Invoke("SpawnRandomCar", spawnInterval);
    }

    // The enumerator used to move the car
    private IEnumerator MoveNewCar(GameObject newCar)
    {
        // Making sure that the object is active
        while (newCar.activeSelf == true)
        {
            // Moves the car and then calls the method that checks to see if it has reached the end
            newCar.transform.Translate(0, 0, 0.1f);
            CheckCarRoute(newCar);
            
            // To close the loop
            yield return null;
        }
    }

    // Method used to check if the car has reached the end of its route
    private void CheckCarRoute(GameObject newCar)
    {
        // Checking the X position of the new car against the X postition of the end object
        if (newCar.transform.position.x >= carRouteEndObject.transform.position.x) 
        { 
            finishedWithCar = true;
            // If it has reached the end, destroy it
            StopCoroutine(MoveNewCar(newCar));
        }
    }
}
