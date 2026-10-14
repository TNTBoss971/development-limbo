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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
