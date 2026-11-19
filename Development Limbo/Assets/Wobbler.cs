using UnityEngine;

public class Wobbler : MonoBehaviour
{
    public enum TransformType {Transform,RectTransform}

    public Vector2 startingPosition;
    public Quaternion startingRotation;

    public TransformType transformType;

    public float verticalIntensity;
    public float horizontalIntensity;
    public float rotationIntensity;
    public float cycleLength;

    [Header("Offset")]

    public bool randomizeOffset;
    public float offsetOveride;
    private float xOffset;
    private float yOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
        xOffset = Random.Range(0,2*Mathf.PI);
        yOffset = Random.Range(0,2*Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        float timeValue = Time.time % cycleLength;
        if (!randomizeOffset)
        {
            xOffset=offsetOveride;
            yOffset=offsetOveride;
        }

        transform.localPosition = startingPosition + new Vector2(Mathf.Sin(Time.time + xOffset) * horizontalIntensity, Mathf.Sin(Time.time + yOffset) * verticalIntensity);
        
    }
}
