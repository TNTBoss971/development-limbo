using UnityEngine;
using System.Collections.Generic;
public class KeyLayoutData : MonoBehaviour
{
    public List<GameObject> children;

    void Start()
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
