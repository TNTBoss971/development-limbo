using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LivesDisplayer : MonoBehaviour
{
    public GameManager gameManager; 
    public GameObject lifePrefab;
    public GameObject shieldPrefab;
    public GameObject[] activeLifeObjects;
    public GameObject[] activeShieldObjects;
    public int maxLives;
    public int maxShields;
    public float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // create life objects
        maxLives = gameManager.playerLives;
        
        // resize the activeLifeObjects array and Instantiate that many new life objects
        activeLifeObjects = new GameObject[maxLives];
        for (int i = 0; i < maxLives; i++)
        {
            GameObject lifeClone = Instantiate(lifePrefab, gameObject.transform);
            // position the object appropriately
            lifeClone.transform.localPosition = new Vector2(i * distance, 0);
            
            activeLifeObjects[i] = lifeClone;
        }

        // create shield objects
        maxShields = gameManager.playerMaxShields;
        
        // resize the activeShieldObjects array and Instantiate that many new shield objects objects
        activeShieldObjects = new GameObject[maxShields];
        for (int i = 0; i < maxShields; i++)
        {
            GameObject shieldClone = Instantiate(shieldPrefab, gameObject.transform);
            // no need for positioning, shields will be positioned on the fly
            
            activeShieldObjects[maxShields-i-1] = shieldClone;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // enable life objects equal to the lives left
        for (int i = 0; i < maxLives; i++)
        {
            if (gameManager.playerLives > i)
            {
                // this object represents a number of lives the player has, show it
                activeLifeObjects[i].SetActive(true);
            }
            else
            {
                // this object represents a number of lives the player doesn't have, hide it
                activeLifeObjects[i].SetActive(false);
            }
        }



        // for all active shields
        for (int i = 0; i < activeShieldObjects.Length; i++)
        {
            /*
            // position the object appropriately
            activeShieldObjects[i].transform.localPosition = new Vector2((i+gameManager.playerLives) * distance, 0);
            */
            // scale and position the object appropriately    
            activeShieldObjects[i].transform.localPosition = new Vector3((gameManager.playerLives-1) * distance, 0, -10);
            float scaleFactor = 0.2f;
            float scale = 1f + scaleFactor * i;
            activeShieldObjects[i].transform.localScale = new Vector3(scale, scale, 1);

            // check if this shield is lost
            if (i <= gameManager.playerShields-1)
            {
                // if it isn't, set active
                activeShieldObjects[i].SetActive(true);
            }
            else
            {
                // if it is, set inactive
                activeShieldObjects[i].SetActive(false);
            }
        }
    }


}
