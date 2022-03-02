using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player_AI : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void TakeBetTurn()
    {
        _player.BetAmount(_player.currentMoney/2);
    }
    
    public void TakePlayTurn()
    {
        if (Random.Range(0, 2) == 0)
        {
            _player.Hit();
            _player.Stand();
        }
        else
        {
            _player.Stand();
        }
    }
}
