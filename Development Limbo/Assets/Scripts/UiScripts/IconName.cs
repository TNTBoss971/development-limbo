using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.InputSystem.InputRemoting;
public class IconName : MonoBehaviour
{
    public static IconName _instance;

    public TextMeshProUGUI _textComponent;

    void Awake()
    {
        // if there is a Icon Nme box already up there, we gotta obliterate it from existence
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        // if not, then let it appear. We need to make sure that there is only 1 Icon Name box.
        else
        {
            _instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowDisplay(string message)
    {
        gameObject.SetActive(true);
        _textComponent.text = message;
    }

    public void HideDisplay()
    {
        gameObject.SetActive(false);
        _textComponent.text = string.Empty;
    }
}
