using UnityEngine;

[CreateAssetMenu(fileName = "ConsistentLives", menuName = "Scriptable Objects/ConsistentLives")]
public class IntSO: ScriptableObject
{
    [SerializeField]private int _paddleLives;

    public int CharacterLives 
    { 
        get { return _paddleLives; } 
        set { _paddleLives = value; } 
    }
}
