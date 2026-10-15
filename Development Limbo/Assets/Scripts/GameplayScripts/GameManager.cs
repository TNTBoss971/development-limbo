using System.Collections.Generic;
using System.Linq;
using Unity.Hierarchy;
using Unity.VisualScripting;
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
    public float spawnDelay; // how long until the next input spawn

    [Header("Scene Objects")]
    public Transform inputObjectsParent;
    public Transform defendPoint;
    public GameObject focusIndicator; // tells the player which input is first in queue

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
            // set the focus indicator to the same position as the target
            focusIndicator.transform.position = targetedInput.transform.position;
        }
        else
        {
            // move the focus indicator far away so it doesn't show up on screen
            focusIndicator.transform.position = new Vector2 (100, 100);
        }

        SpawnLogic();
    }

    public void SpawnLogic()
    {
        if (spawnDelay <= Time.time)
        {
            inputObjectQueue.Add(CreateKey());
            spawnDelay = Time.time + Random.Range(0.1f, 1f);
        }
    }

    void AttackPerformed(InputAction.CallbackContext context)
    {
        // will be a list of all keys pressed
        List<string> keysPressed = new List<string>();
        // check if there is an active keyboard
        if (Keyboard.current != null) 
        {
            // for all keys pressed this frame
            foreach (var control in Keyboard.current.allControls)
            {
                // set a temporary key variable and check whether it was pressed this frame
                if (control is KeyControl key && key.wasPressedThisFrame)
                {
                    // add the display name of the key, as that is all we need, to the list
                    keysPressed.Add(key.displayName);
                }
            }
            // this bit of code exist to confirm in the console the keys were pressed
            // as of 10/12/2026, I'm not deleting this becuase it might be helpful later
            // and I don't really wanna redo this lol
            string tempString = "";
            foreach (var key in keysPressed)
            {
                tempString = tempString + key;
            }
            Debug.Log(tempString);
        }

        // exit the function; this function can sometimes trigger when a key is release but another is still held
        // when that happens, keyPressed is empty, so it'll be useless to let the function continue
        // besides we don't want to register released keys, only pressed ones.
        if (keysPressed.Count == 0)
        {
            return;
        }
        
        // now that it has all keys pressed this frame, the next step is to iterate through it and compare it the inputObjectQueue
        foreach (var key in keysPressed)
        {
            // get the targetedInput's controller, as that is what we will need the most
            InputObjectController targetedController = targetedInput.GetComponent<InputObjectController>();
            if (targetedController.assignedKey == key)
            {
                // play a fun particle!! (THIS IS A JUICE HOUSEHOLD!!!)
                CreateParticleEffect("inputObjectDestroyed", targetedInput.transform);
                // Lmao idiot dies to a key press (Destory the targetedInput)
                Destroy(targetedInput);
                AlterPlayerValue("score", 1);

                // if the inputObjectQueue isn't empty (if it only has one item, then it would be the object we just destroyed)
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
            else
            {
                // player messed up, so let's punish them!!!

                // maybe change the color pallet to red/some other hostile color?

                CreateParticleEffect("incorrectInput", targetedInput.transform);
                AlterPlayerValue("lives", -1);
            }
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
        clone.GetComponent<InputObjectController>().assignedKey = PickKey();
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


    public void AlterPlayerValue(string valueId, float alterAmount)
    {
        // I know I *could* just do something like "value+=1"
        // but if I want certain particles to always play whenever the value is altered,
        // then a centralized function where all of that is controlled is ideal

        if (valueId == null)
        {
            // you moron Sam, how could you have forgot the id?
        }
        else if (valueId == "score")
        {
            // alter the score
            playerScore += (int)alterAmount;
            // then find the desired effect and play it
            if (alterAmount > 0)
            {
                // yay! yippee! hooray!
            }
            else
            {
                // :( life is awful and everthing hurts. without my score, its just not worth it anymore
            }
        }
        else if (valueId == "lives")
        {
            // alter the lives
            playerLives += (int)alterAmount;
            // then find the desired effect and play it
            if (alterAmount > 0)
            {
                // unlikely we'll add a life gain mechanic, but here is it's spot if we do
            }
            else
            {
                // ouch
            }
        }
    }

    public void CreateParticleEffect(string particleId, Transform locationInfo)
    {
        // instatiate a particle at the specified transform's position
        // this function asks for a transform and not a Vector2, becuase I forsee some partice effect requiring rotation
    }

    public string PickKey()
    {
        /*
        Returns a key as a string
        */
        return tempKeys[Random.Range(0,tempKeys.Length)];
    }
}
