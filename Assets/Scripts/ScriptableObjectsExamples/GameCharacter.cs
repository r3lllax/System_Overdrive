using UnityEngine;

[CreateAssetMenu(fileName = "CurrentCharacter", menuName = "ScriptableObjects/Character")]
public class GameCharacter : ScriptableObject
{
    public string CharacterName;
    public int Health;
    public float MoveSpeed;
    public GameObject CharacterPrefab;   
}

