using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenuManager : MonoBehaviour
{
    // Inspector Assigned Variables
    [SerializeField] protected Image _fadeBg = null;
    [SerializeField] protected float _fadeSpeed = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        // make it interactable
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;


        // the Fade Panel Must be oaque at Start
        _fadeBg.color = new Color(0f, 0f, 0f, 1f);

        // make the game fade in at opening. More Streamlined...
        StartCoroutine(FadeIn(true, _fadeSpeed));
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
            _fadeBg.color = Color.Lerp(startColor, targetColor, timing / speed);

            // Increment timing by the time passed since last frame.
            timing += Time.deltaTime;

            yield return null;
        }
        // just as an extra precaution
        _fadeBg.color = targetColor;
    }


    // Button Functions: These call Button is pressed. Sets up stage for fade.
    public void NewGame()
    {
        // Starts the game and loads a new scene.

        // Can't have the player goofing off and crashing the game.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // call the coroutine to fade out the screen and load the new scene.
        StartCoroutine(FadeIn(false, _fadeSpeed));

        // we wait for the Fade to pass, then we start new scene.
        Invoke ("LoadGame", _fadeSpeed);
    }

    public void Credits()
    {
        // Starts the game and loads a new scene.

        // Can't have the player goofing off and crashing the game.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // call the coroutine to fade out the screen and load the new scene.
        StartCoroutine(FadeIn(false, _fadeSpeed));

        // we wait for the Fade to pass, then we start new scene.
        Invoke("CreditsScene", _fadeSpeed);
    }


    public void Quit()
    {
        // Starts the game and loads a new scene.

        // Can't have the player goofing off and crashing the game.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // call the coroutine to fade out the screen and load the new scene.
        StartCoroutine(FadeIn(false, _fadeSpeed));

        // we wait for the Fade to pass, then we start new scene.
        Invoke("QuitGame", _fadeSpeed);
    }



    // Button Functions: These are called after the fade is complete. Loads the new scene.


    public void LoadGame()
    {
        // Loads the new scene after the fade out is complete.
        SceneManager.LoadScene("Gameplay");
    }

    public void CreditsScene()
    {
        // Loads the new scene after the fade out is complete.
        SceneManager.LoadScene("Closing Credits");
    }

    public void QuitGame()
    {
        // Quits the game after the fade out is complete.
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }
}
