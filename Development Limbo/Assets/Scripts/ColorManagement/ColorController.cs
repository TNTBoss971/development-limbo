using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public enum RenderType {Image, SpriteRenderer, Light, TextMeshProUGUI, TextMeshPro}
    public RenderType type;
    [Header("Managers")]
    private ColorManager colorManager;

    public bool isBackground;
    private Color currentColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorManager = FindAnyObjectByType<ColorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBackground)
        {
            currentColor = colorManager.activeBackgroundColor;
        }
        else
        {
            currentColor = colorManager.activeColor;
        }

        if (type == RenderType.Image)
        {
            GetComponent<Image>().color = currentColor;
        }
        if (type == RenderType.SpriteRenderer)
        {
            GetComponent<SpriteRenderer>().color = currentColor;
        }
        if (type == RenderType.Light)
        {
            GetComponent<Light>().color = currentColor;
        }
        if (type == RenderType.TextMeshProUGUI)
        {
            GetComponent<TextMeshProUGUI>().color = currentColor;
        }
        if (type == RenderType.TextMeshPro)
        {
            GetComponent<TextMeshPro>().color = currentColor;
        }
    }
}
