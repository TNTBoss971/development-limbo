using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputObjectController : MonoBehaviour
{
    public string assignedKey; // the key the player needs to press in order to attack this
    public TextMeshPro textMeshPro; // the display text

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
}
