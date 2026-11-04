using UnityEngine;

public class Icon : MonoBehaviour
{
    public string message;

    public void OnMouseEnter()
    {
        IconName._instance.ShowDisplay(message);
    }

    public void OnMouseExit()
    {
        IconName._instance.HideDisplay();
    }
}
