using System.Linq;
using TMPro;
using UnityEngine;

public class ValueDisplayer : MonoBehaviour
{
    private GameManager gameManager;
    private TextMeshProUGUI textMeshPro; // the display text

    public string valueString;
    public string templateString;
    public enum DisplayValue { Score,Accuracy,Lives }
    public DisplayValue value;

    public bool addZerosAtEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        textMeshPro = GetComponent<TextMeshProUGUI>(); // gets the text mesh
    }

    // Update is called once per frame
    void Update()
    {
        // count the number of placeholder characters there are
        // this is not only determines how many 0s we need, but also the max length of the value string
        int valueCharacterCount = templateString.Split('#').Length - 1;

        // constantly find and set the stored value
        // score
        if (value == DisplayValue.Score)
        {
            valueString = gameManager.playerScore.ToString();
        }
        // accuracy
        if (value == DisplayValue.Accuracy)
        {
            valueString = (gameManager.playerAccuracy*100).ToString();
        }
        // lives
        if (value == DisplayValue.Lives)
        {
            valueString = gameManager.playerLives.ToString();
        }
        // if valueString is too long (should really only happen with long floats like Accuracy) then trim it
        if (valueString.Count() > valueCharacterCount)
        {
            valueString = valueString.Substring(0, valueCharacterCount - 1);
        }

        // find first portion
        string displayText = templateString.Split('#')[0];

        if (addZerosAtEnd)
        {
            displayText += valueString;
        }

        for (int i = 0; i < valueCharacterCount - valueString.Length; i++)
        {
            displayText += "0";
        }

        if (!addZerosAtEnd)
        {
            displayText += valueString;
        }
        textMeshPro.text = displayText;
    }
}
