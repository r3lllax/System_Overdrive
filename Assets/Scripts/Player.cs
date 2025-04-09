using UnityEngine;

public class Player : MonoBehaviour
{
    //Обращаемся к классу TEMPDATA и в переменную SpeedMultiply получаем множитель скорости с оружием
    
    private float PlayerSpeedMultiplier = 1f;

    void Start()
    {
        GetComponent<PlayerController>().SetPlayerMSWithMultiplier(PlayerSpeedMultiplier);
         
    }
}
