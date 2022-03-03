using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBet_UI : MonoBehaviour
{
    public GameObject bet_UI;
    
    public void PlaceBet(int amount)
    {
        GameObject.FindGameObjectWithTag("Player_S").GetComponent<Player>().BetAmount(amount);
    }
}
