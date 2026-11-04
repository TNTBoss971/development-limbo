using UnityEngine;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        None,
        MainMenu,
        Gameplay
    }

    public State currentState;


    public LayerMask gameplayLayers;
    public LayerMask mainMenuLayers;
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.MainMenu)
        {
            cam.cullingMask = mainMenuLayers;
        }
        if (currentState == State.Gameplay)
        {
            cam.cullingMask = gameplayLayers;
        }
    }
}
