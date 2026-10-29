using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Color mainColor0;
    public Color backgroundColor0;
    public Color mainColor1;
    public Color backgroundColor1;

    public Color activeColor;
    public Color activeBackgroundColor;
    public bool hasSwapped;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeColor = mainColor0;
        activeBackgroundColor = backgroundColor0;
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
            activeBackgroundColor = backgroundColor0;
        }
        else
        {
            activeColor = mainColor1;
            activeBackgroundColor = backgroundColor1;
        }
        // invert hasSwapped
        hasSwapped = !hasSwapped;
    }
}
