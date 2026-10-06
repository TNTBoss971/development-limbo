using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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
    [Header("Input Actions")]

    public InputAction attackAction;

    [Header("TEMPORARY")]
    //temporary 
    public string[] tempKeys;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObjectQueue.Add(CreateKey());

        // define input actions
        attackAction = InputSystem.actions.FindAction("AttackInput");
        attackAction.performed += AttackPerformed;
    }


    // Update is called once per frame
    void Update()
    {
        // if the queue has an item in it, set the target input to the first item
        if (inputObjectQueue.Count > 0) {
            targetedInput = inputObjectQueue[0];
        }

        // get input actions
    }
    void AttackPerformed(InputAction.CallbackContext context)
    {
        string[] keysPressedThisFrame = [];

        if (Keyboard.current != null)
        {
            foreach (var control in Keyboard.current.allControls)
            {
                if (control is KeyControl key && key.wasPressedThisFrame)
                {
                    keysPressedThisFrame.Append(key.displayName);
                }
            }
        }

        // IF the player has started pressing a key this frame:
            // SET string pressed_key as that key as text
            // IF pressed_key is the same as targeted_input’s stored key:
                // Play some fun particle effects at targeted_input’s position!
                // Destroy targeted_input, and shift all items in input_queue 1 towards the front.
                // SET player_score to player_score + 1
            // ELSE:
                // SET player_lives to player_lives - 1
                // Play some screenshake, increasing the less lives the player has.
    }

    public GameObject CreateKey()
    {
        /*
        This function returns a created Input GameObject
        */
        // clone the prefab
        GameObject clone = Instantiate(inputObjectPrefab, inputObjectsParent);
        // and assign its key
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
