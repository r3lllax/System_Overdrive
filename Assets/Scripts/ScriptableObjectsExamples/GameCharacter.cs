using UnityEngine;

[CreateAssetMenu(fileName = "CurrentCharacter", menuName = "ScriptableObjects/Character")]
public class GameCharacter : ScriptableObject
{
    public string CharacterName;
    public int Health;
    public float MoveSpeed;
    public GameObject CharacterPrefab;
    public GameObject AbilityPrefab;
    public string AbilityDescription;
    public float ActiveTime;
    public float CooldownTime;
    public float CastTime;
}

