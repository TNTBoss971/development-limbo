using UnityEngine;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    public enum RenderType {Image, SpriteRenderer}
    public RenderType type;
    [Header("Managers")]
    private ColorManager colorManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorManager = FindAnyObjectByType<ColorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (type == RenderType.Image)
        {
            GetComponent<Image>().color = colorManager.activeColor;
        }
        if (type == RenderType.SpriteRenderer)
        {
            GetComponent<SpriteRenderer>().color = colorManager.activeColor;
        }
    }
}
