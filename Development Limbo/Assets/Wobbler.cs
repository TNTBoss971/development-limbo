using Unity.Mathematics;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.localPosition;
        startingRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float timeValue = Time.time % cycleLength;

        if (transformType == TransformType.Transform)
        {
            transform.position = startingPosition + new Vector2(timeValue * horizontalIntensity, timeValue*verticalIntensity);
        }
    }
}
