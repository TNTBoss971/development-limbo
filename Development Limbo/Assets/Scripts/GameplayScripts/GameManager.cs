using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    public int playerLives; // stores how many misses the player has left
    public int playerScore = 0; // stores player score

    [Header("Input Objects Info")]
    public List<GameObject> inputObjectQueue; // stores input objects in order of creation
    public GameObject targetedInput; // always the first object in inputObjectQueue

    [Header("Scene Objects")]
    public Transform inputObjectsParent;

    [Header("Prefabs")]
    // needed prefabs
    public GameObject inputObjectPrefab; // the prefab for all spawned input objects

    [Header("TEMPORARY")]
    //temporary 
    public string[] tempKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateKey();
    }

    // Update is called once per frame
    void Update()
    {
        targetedInput = inputObjectQueue[0];
    }

    public GameObject CreateKey()
    {
        /*
        This function returns a created Input GameObject
        */
        GameObject clone = Instantiate(inputObjectPrefab, inputObjectsParent);
        clone.GetComponent<InputObjectController>().assignedKey = PickKey();
        return clone;
    }
    public string PickKey()
    {
        /*
        Returns a key as a string
        */
        return tempKeys[Random.Range(0,9)];
    }
}
