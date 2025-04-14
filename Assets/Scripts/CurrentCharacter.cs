using UnityEngine;

public class CurrentCharacter : MonoBehaviour
{
    private GameObject Player;
    void Awake()
    {
        SessionData.ShowData();
        Player = TempData.ChoosenCharacter.CharacterPrefab;
        var playerCharacter = Instantiate(Player,gameObject.transform.position,Quaternion.identity);
        playerCharacter.transform.parent = gameObject.transform;
    }
}
