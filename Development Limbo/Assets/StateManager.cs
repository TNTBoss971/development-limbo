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

    private GameManager gameManager;

    public LayerMask gameplayLayers;
    public LayerMask mainMenuLayers;
    public Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GetComponentInChildren<GameManager>();

        if (currentState == State.Gameplay)
        {
            gameManager.StartGame();
        }
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


    [ContextMenu("Enter Gameplay State")]
    public void DebugEnterGameplayAction()
    {
        ActivateState(State.Gameplay);
    }
    [ContextMenu("Enter Main Menu State")]
    public void DebugEnterMainMenuAction()
    {
        ActivateState(State.MainMenu);
    }

    public void ActivateState(State targetState)
    {
        if (targetState == State.Gameplay)
        {
            gameManager.StartGame();
        }
        currentState = targetState;
    }
}
