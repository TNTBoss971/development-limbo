using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputObjectController : MonoBehaviour
{
    public string assignedKey; // the key the player needs to press in order to attack this
    private TextMeshPro textMeshPro; // the display text
    public Transform target; // defend point's position


    public Vector2 startPosition;
    public float startTime;
    public float passedTime; // time difference, basically how much time has passed since Instantiation
    public Vector2 travelVector;
    public float travelTime;

    public InputObjectManager manager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
        manager = FindAnyObjectByType<InputObjectManager>();

        startPosition = transform.position;
        startTime = Time.time;
        
        // get the vector that this object will travel
        travelVector = (Vector2)target.position - startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = assignedKey;
        

        // calculate time since Instantiation
        passedTime = Time.time - startTime;
        // starting from the startPosition, set position to be along a vector at a distance equal to how long it has been since Instantiation
        // that is probably not helpfull, sorry :(
        transform.position = startPosition + travelVector * (passedTime / travelTime);
    }

}
