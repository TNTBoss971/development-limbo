using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;
public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    public int playerLives; // stores how many misses the player has left
    public int playerScore = 0; // stores player score

    [Header("Input Objects Info")]
    public List<GameObject> inputObjectQueue; // stores input objects in order of creation
    public GameObject targetedInput; // always the first object in inputObjectQueue
    public float spawnDelay; // how long until the next input spawn

    [Header("\"Drifting Point\" Logic")]
    public float pointX;
    public float pointY;
    public Vector2 targetVector;
    public float targetTime; // not an AMOUNT of time but rather a POINT in time
    public float gameStartTime; // helps track how long gameplay has been running

    public GameObject keyLayoutObject;
    public KeyObjectData[] keyObjectDatas;
    public float distanceScaleExponent;
    public float distanceCutoff;


    [Header("Scene Objects")]
    public Transform inputObjectsParent;
    public Transform defendPoint;
    public GameObject focusIndicator; // tells the player which input is first in queue

    [Header("Managers")]
    public ColorManager colorManager;


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

        // setup the layout with QWERTY
        SetupLayout("QWERTY");


        gameStartTime = Time.time;
        PickRandomTarget();
    }

    /*
    START:
	    PICK_RANDOM_TARGET


    FIXED_UPDATE:
	    
    */


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
    
    void FixedUpdate()
    {
        PointMovement();   
    }

    public void AttackPerformed(InputAction.CallbackContext context)
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
                // swap color scheme
                colorManager.ColorSwap();
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
    public string PickKey()
    {
        /*
        Returns a key as a string
        */

        float selectedNum = Random.Range(0f, 1f);
        float totalProbabilty = 0f;
        // for each key, see if it was chosen
        foreach (KeyObjectData keyInfo in keyObjectDatas)
        {
            if (keyInfo.isIncluded)
            {
                totalProbabilty += keyInfo.probabilty;
                if (totalProbabilty >= selectedNum)
                {
                    Debug.Log("Key: " + keyInfo.key + " Prob: " + keyInfo.probabilty*100 + " Dist: " + keyInfo.distance + " ScalDist: " + keyInfo.scaledDistance);
                    return keyInfo.key;
                }
            }
        }

        return null;
    }

    public void PickRandomTarget()
    {
        // get a random point with range to head towards
        float targetX = Random.Range(-5.5f, 4f);
        float targetY = Random.Range(-0.5f, 1f);
        // get a vector from the current point pointing to the random point and target to that
        targetVector = new Vector2(targetX - pointX, targetY - pointY);
        // NOPE OUTDATED LOL > set target time to a number based on current game length(lower the longer) plus the current time
        //targetTime = 60 / ((Time.time - gameStartTime) / 10 + 10f) + Time.time;
        targetTime = 10f + Time.time;
    }

    public void PointMovement() // move the point that calculates key probabilty
    {
        // move the point (target vector / total time alloted for this movement / number of times FixedUpdate fires in a second)
        pointX += targetVector.x / 10 / 50;
        pointY += targetVector.y / 10 / 50;
        // change direction if it is time to do so
        if (Time.time > targetTime)
        {
            PickRandomTarget();
        }

        // used for calculating probablity
        float totalDistance = 0;
        // for each keyInfo find their distance to the point, then add it to total
        foreach (KeyObjectData keyInfo in keyObjectDatas)
        {
            //Debug.Log("Key at " + keyInfo.keyX + ", " + keyInfo.keyY);

            // get the distance to the point
            Vector2 differenceVector = new Vector2 (pointX - keyInfo.keyX, pointY - keyInfo.keyY);

            // set distance to the found distance
            keyInfo.distance = differenceVector.magnitude;
            // invert it because otherwise the further the key is the more likely, and we want the other way around
            // additionally, raise it an exponent to make the closer keys even more likely.
            keyInfo.scaledDistance = 1 / Mathf.Pow(keyInfo.distance, distanceScaleExponent);
            
            // is the found scaled distance large enough that it actually matters
            if (keyInfo.scaledDistance > distanceCutoff)
            {
                // add distance to total
                totalDistance += keyInfo.scaledDistance;
                // include
                keyInfo.isIncluded = true;
            }
            else
            {
                // exclude
                // loser
                keyInfo.isIncluded = false;
            }
        }
        // once again iterate through layout, this finding probabilty of each key
        foreach (KeyObjectData keyInfo in keyObjectDatas)
        {
            // do we actually care about the key this frame?
            if (keyInfo.isIncluded)
            {
                keyInfo.probabilty = keyInfo.scaledDistance / totalDistance;
            }
        }
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

    public void SetupLayout(string layout)
    {
        char[] layoutArray = null;
        string layoutText = "";
        if (layout == "QWERTY")
        {
            layoutText = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            layoutArray = layoutText.ToCharArray();
        }

        // make sure that the keyLayoutObject is setup
        keyLayoutObject.GetComponent<KeyLayoutData>().Setup();
        // get all key objects in scene as an array
        List<GameObject> keyObjects = keyLayoutObject.GetComponent<KeyLayoutData>().children;
        int index = 0;
        // iterate through that array, adding their KeyObjectData to the layout and setting it's key
        foreach (GameObject keyObject in keyObjects)
        {
            KeyObjectData keyObjectData = keyObject.GetComponent<KeyObjectData>();
            // add the key object to layout
            // (I know a list would be easier, but I want to use an array becuase the length won't change after this function finishes)
            keyObjectDatas = keyObjectDatas.Append(keyObjectData).ToArray();
            // set the object's key
            keyObjectData.key = layoutArray[index].ToString();
            // send the all clear to the key object
            keyObjectData.Setup();
            index++;
        }



    }
}
