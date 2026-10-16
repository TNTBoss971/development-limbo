
using UnityEngine;


[CreateAssetMenu(fileName = "KeyLayout", menuName = "Scriptable Objects/KeyLayout")]
[System.Serializable]
public class KeyLayout : ScriptableObject
{
    public KeyInfo[] keyInfos;
}
