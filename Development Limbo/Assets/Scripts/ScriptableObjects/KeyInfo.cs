using UnityEngine;

[CreateAssetMenu(fileName = "KeyInfo", menuName = "Scriptable Objects/KeyInfo")]
[System.Serializable]
public class KeyInfo : ScriptableObject
{
    public string key;
    public float xPos;
    public float yPos;

    public float distance;
    public float probabilty;
}
