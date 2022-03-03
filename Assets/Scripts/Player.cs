using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerPositions
    {
        N,
        E,
        S,
        W,
        None
    }

    public PlayerPositions playerPosition;
    public bool isHumanPlayer;
    public int currentMoney;
    
    private PlayerBet_UI _playerBetUI;
    private List<int> _playerCards;
    private List<Transform> _cardPositions;
    private bool _isPlayerTurn;

    private void Awake()
    {
        _playerCards = new List<int>();
        _playerBetUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PlayerBet_UI>();
    }
    
    private void Update()
    {
        if (!isHumanPlayer) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Stand();
        }
    }

    #region Bet Phase Functions
    public void BetTurnStart()
    {
        if(!isHumanPlayer) GetComponent<Player_AI>().TakeBetTurn();
        else
        {
            _playerBetUI.bet_UI.SetActive(true);
        }
    }
    
    public void BetAmount(int amount)
    {
        int betAmount = amount > currentMoney ? currentMoney : amount;
        currentMoney -= betAmount;
        GameManager2.current.PlaceBet(playerPosition, betAmount);

        if (!isHumanPlayer) return;
        _playerBetUI.bet_UI.SetActive(false);
    }
    #endregion
    
    #region Play Phase Functions

    public void PlayTurnStart()
    {
        _isPlayerTurn = true;
        
        if(!isHumanPlayer) GetComponent<Player_AI>().TakePlayTurn();
    }
    
    public void Hit()
    {
        GameManager2.current.PlayerHit(playerPosition);
    }

    public void Stand()
    {
        GameManager2.current.PlayerStand(playerPosition);
        _isPlayerTurn = false;
    }
    #endregion

    #region End Phase Functions

    public void GainMoney(int amount)
    {
        currentMoney += amount;
    }

    public void LoseMoney(int amount)
    {
        currentMoney -= amount;
    }
    #endregion
}
