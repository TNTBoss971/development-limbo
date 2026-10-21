using TMPro;
using UnityEngine;

public class KeyObjectData : MonoBehaviour
{
    public string key;
    public float distance;
    public float scaledDistance;
    public float probabilty;

    public float keyX;
    public float keyY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyX = transform.localPosition.x;
        keyY = transform.localPosition.y;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(probabilty, probabilty, probabilty);
    }
    public void Setup() // called by the game manager when it is all clear to setup
    {
        this.gameObject.GetComponentInChildren<TextMeshPro>().text = key;
    }
}
