using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class KeyLayoutData : MonoBehaviour
{
    public List<GameObject> children;

    void Start()
    {
        
    }
    public void Setup() // called by the game manager when it is all clear to setup
    {
        // get all children
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "KeyObject")
            {
                children.Add(child.gameObject);
            }
        }
    }
}
