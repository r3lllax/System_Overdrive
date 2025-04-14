using UnityEngine;

[CreateAssetMenu(fileName = "CurrentCharacter", menuName = "ScriptableObjects/Character")]
public class GameCharacter : ScriptableObject
{
    public string CharacterName;
    public float Health;
    public float MoveSpeed;
    public GameObject CharacterPrefab;   
}

