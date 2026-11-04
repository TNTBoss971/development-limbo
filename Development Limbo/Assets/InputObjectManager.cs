using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class InputObjectManager : MonoBehaviour
{
    private GameManager gameManager;
    private StateManager stateManager;
    public Transform defendPoint;
    public Transform inputObjectsParent;

    [Header("Input Objects Info")]
    public List<GameObject> inputObjectQueue; // stores input objects in order of creation
    public GameObject targetedInput; // always the first object in inputObjectQueue
    public float spawnDelay; // how long until the next input spawns

    [Header("Prefabs")]
    // needed prefabs
    public GameObject inputObjectPrefab; // the prefab for all spawned input objects

    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GetComponentInParent<GameManager>();

        stateManager = gameManager.stateManager;
    }

    // Update is called once per frame
    void Update()
    {

        if (stateManager.currentState == StateManager.State.Gameplay)
        {
            if (inputObjectQueue.Count > 0)
            {
                targetedInput = inputObjectQueue[0];
            }
            else
            {
                targetedInput = null;
            }

            SpawnLogic();
        }
    }
    public void SpawnLogic()
    {
        if (spawnDelay <= Time.time)
        {
            inputObjectQueue.Add(CreateKey());
            spawnDelay = Time.time + Random.Range(0.1f, 1f);
        }
    }

    
    public GameObject CreateKey()
    {
        /*
        This function returns a created Input GameObject
        */
        // clone the prefab
        GameObject clone = Instantiate(inputObjectPrefab, inputObjectsParent);
        // and assign its key
        clone.GetComponent<InputObjectController>().assignedKey = gameManager.PickKey();
        // for now, set it a random position
        clone.transform.position = new Vector2(Random.Range(-9,9), Random.Range(-5,5));

        // circular positioning

        // get a random degree from between 45 and 135, then convert to radians
        float rot = Random.Range(45, 135) / 180f * Mathf.PI;
        // get the direction as a vector
        Vector2 rotAsVec = new Vector2(Mathf.Cos(rot), Mathf.Sin(rot));
        // set the clone's starting position to the direction vector multiplied by starting distance offset by defendPoint's position
        float startingDistance = 20f;
        clone.transform.position = new Vector2 (rotAsVec.x * startingDistance + defendPoint.transform.position.x, rotAsVec.y * startingDistance + defendPoint.transform.position.y);
        // set target to defend point
        clone.GetComponent<InputObjectController>().target = defendPoint.transform;

        return clone;
    }

    public void ShiftQueue()
    {
        // if the inputObjectQueue isn't empty (if it only has one item, then the list would have to be set to nothing)
        if (inputObjectQueue.Count > 1)
        {
            // move the inputObjectQueue up so that there isn't null in the list
            inputObjectQueue = inputObjectQueue.GetRange(1, inputObjectQueue.Count-1);
        }
        else
        {
            inputObjectQueue = new List<GameObject>();
            targetedInput = null;
        }
    }
}
