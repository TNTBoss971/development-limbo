using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color mainColor0;
    public Color backgroundColor0;
    public Color mainColor1;
    public Color backgroundColor1;

    public Color activeColor;
    public Color contrastingColor;
    public bool hasSwapped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeColor = mainColor0;
        contrastingColor = mainColor1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ColorSwap()
    {
        if (hasSwapped)
        {
            activeColor = mainColor0;
            contrastingColor = mainColor1;
        }
        else
        {
            activeColor = mainColor1;
            contrastingColor = mainColor0;
        }
        // invert hasSwapped
        hasSwapped = !hasSwapped;
    }
}
