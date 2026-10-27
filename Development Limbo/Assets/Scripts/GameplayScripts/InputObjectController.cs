using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputObjectController : MonoBehaviour
{
    public string assignedKey; // the key the player needs to press in order to attack this
    private TextMeshPro textMeshPro; // the display text

    public Transform target; // defend point's position
    public Vector2 targetVector; // normalized movement vector

    public float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = assignedKey;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            // get the direction to target
            targetVector = (target.position - this.transform.position).normalized;
            // move towards target
            this.transform.position = new Vector2(transform.position.x + targetVector.x * speed, transform.position.y + targetVector.y * speed);
        }
    }
}
