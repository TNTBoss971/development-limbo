using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class DevLogsManager : MonoBehaviour
{
    // Inspector Variables
    [SerializeField] protected List<TextAsset> _devLogs = new List<TextAsset>();

    // Internal Variables
    private GameManager _gameManager;
    private MainMenuManager _mainMenuManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
