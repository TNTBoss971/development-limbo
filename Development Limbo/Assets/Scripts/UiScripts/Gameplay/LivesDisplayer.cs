using System;
using System.Collections.Generic;
using UnityEngine;

public class LivesDisplayer : MonoBehaviour
{
    public GameManager gameManager; 
    public GameObject lifePrefab;
    public GameObject[] activeLifeObjects;
    public int maxLives;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxLives = gameManager.playerLives;
        
        // resize the activeLifeObjects array and Instantiate that many new life objects
        activeLifeObjects = new GameObject[maxLives];
        for (int i = 0; i < maxLives; i++)
        {
            GameObject lifeClone = Instantiate(lifePrefab, gameObject.transform);
            // position the object appropriately
            lifeClone.transform.localPosition = new Vector2(i * 30, 0);
            
            activeLifeObjects[i] = lifeClone;
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
    }
}
