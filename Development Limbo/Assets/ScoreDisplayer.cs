using TMPro;
using UnityEngine;

public class ScoreDisplayer : MonoBehaviour
{
    public GameManager gameManager;
    public string scoreString;

    public TextMeshProUGUI textMeshPro; // the display text

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>(); // gets the text mesh
    }

    // Update is called once per frame
    void Update()
    {
        // constantly set the stored score
        scoreString = gameManager.playerScore.ToString();

        string displayText = "score: ";

        for (int i = 0; i < 5 - scoreString.Length; i++)
        {
            displayText += "0";
        }
        displayText += scoreString;
        textMeshPro.text = displayText;
    }
}
