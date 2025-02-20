using UnityEngine;

[CreateAssetMenu(fileName = "ConsistentLives", menuName = "Scriptable Objects/ConsistentLives")]
public class IntSO: ScriptableObject
{
    [SerializeField]private int paddleLives;

    public int CharacterLives 
    { 
        get { return paddleLives; } 
        set { paddleLives = value; } 
    }
}
