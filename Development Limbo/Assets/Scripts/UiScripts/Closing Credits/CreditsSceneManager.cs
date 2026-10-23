using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsSceneManager : MonoBehaviour
{
    // Inspector Assignable Variables
    [SerializeField] protected RectTransform _creditsContainer;
    [SerializeField] protected Image _fade;
    [SerializeField] protected float _finalLocation;
    [SerializeField] protected float _scrollSpeed = 0.1f;
    [SerializeField] protected float _fadeSpeed = 1.5f;
    [SerializeField] protected float _fadeOutro = 3.5f;

    // Internal Variables
    protected Vector3 _initialPos;

    private void Start()
    {
        _initialPos = _creditsContainer.localPosition;

        _fade.color = new Color(0f, 0f, 0f, 1f);

        // sets up the fade panel for the credits scene. It shall fade in at the start of the scene
        StartCoroutine(FadeIn(true, _fadeSpeed));

        // Start the credits scrolling
        StartCoroutine(CreditRoll());
    }
    

    IEnumerator CreditRoll()
    {
        float timer = 0f;
        
        // Responsible for rolling the credits
        while (timer < _scrollSpeed)
        {
            Vector3 containerPosition = _initialPos;


            // Takes initial position and then lerps it so that it moves uniformy through the scene.
            containerPosition.y  = Mathf.Lerp(_initialPos.y, _finalLocation, timer / _scrollSpeed);

            _creditsContainer.localPosition = containerPosition;

            // updates the timer with the time since the last frame.
            timer += Time.deltaTime;

            // if player wants to escape the credits, they just press escape and it will fade out the credits and load the main menu.
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // If the Escape key is pressed, exit the credits scene
                StartCoroutine(FadeIn(false, _fadeSpeed));

                Invoke("LoadMainMenu", _fadeSpeed);
            }
            yield return null;
        }
        // Once Credits Roll, it automaticall goes to main menu

        StartCoroutine(FadeOut(false, _fadeOutro));

        Invoke("LoadMainMenu", _fadeOutro);
    }

    IEnumerator FadeIn(bool fadeIn, float speed)
    {
        // initializes all values
        Color targetColor = fadeIn ? new Color(0f, 0f, 0f, 0f) : new Color(0f, 0f, 0f, 1f);
        Color startColor = fadeIn ? new Color(0f, 0f, 0f, 1f) : new Color(0f, 0f, 0f, 0f);

        float timing = 0.0f;

        while (timing <= _fadeSpeed)
        {
            // Loop runs until target color is reached.
            _fade.color = Color.Lerp(startColor, targetColor, timing / speed);

            // Increment timing by the time passed since last frame.
            timing += Time.deltaTime;

            yield return null;
        }
        // just as an extra precaution
        _fade.color = targetColor;
    }

    // This is just like the Fade in, but it is specifically for the outro so it's much more customizable.s
    IEnumerator FadeOut(bool fadeIn, float speed)
    {
        // initializes all values
        Color targetColor = fadeIn ? new Color(0f, 0f, 0f, 0f) : new Color(0f, 0f, 0f, 1f);
        Color startColor = fadeIn ? new Color(0f, 0f, 0f, 1f) : new Color(0f, 0f, 0f, 0f);

        float timing = 0.0f;

        while (timing <= _fadeOutro)
        {
            // Loop runs until target color is reached.
            _fade.color = Color.Lerp(startColor, targetColor, timing / speed);

            // Increment timing by the time passed since last frame.
            timing += Time.deltaTime;

            yield return null;
        }
        // just as an extra precaution
        _fade.color = targetColor;
    }

    public void LoadMainMenu()
    {
        // Load the main menu scene
        SceneManager.LoadScene("Main Menu");
    }
}
