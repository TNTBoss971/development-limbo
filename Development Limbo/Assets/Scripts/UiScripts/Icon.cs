using UnityEngine;

public class Icon : MonoBehaviour
{
    public string message;

    private void OnMouseEnter()
    {
        IconName._instance.ShowDisplay(message);
    }

    private void OnMouseExit()
    {
        IconName._instance.HideDisplay();
    }
}
