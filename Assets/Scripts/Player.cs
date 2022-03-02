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
    private bool _isPlayerTurn;
    private List<int> _playerCards;
    private List<Transform> _cardPositions;

    private void Awake()
    {
        _playerCards = new List<int>();
        _playerBetUI = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PlayerBet_UI>();
        GetCardPositions();
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
    
    #region Turn End Events
    private void EndTurn()
    {
        GameManager.current.PlayerTurnEnd(playerPosition);
    }
    #endregion
    
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
        if (GameManager.current.GetCurrentGameState() != GameManager.GameState.Bet) return;

        int betAmount = amount > currentMoney ? currentMoney : amount;
        currentMoney -= betAmount;
        GameManager.current.BetAmount(playerPosition, betAmount);
        
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
        if (!CanAcceptCard()) return;
        
        GameManager.current.DealCard(this);
    }

    public void Stand()
    {
        EndTurn();
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
    
    #region Card Functions
    private void GetCardPositions()
    {
        switch(playerPosition)
        {
            case PlayerPositions.N:
                _cardPositions = CardPositions.current.dealerPositions;
                break;
            case PlayerPositions.E:
                _cardPositions = CardPositions.current.E_PlayerPositions;
                break;
            case PlayerPositions.S:
                _cardPositions = CardPositions.current.S_PlayerPositions;
                break;
            case PlayerPositions.W:
                _cardPositions = CardPositions.current.W_PlayerPositions;
                break;
        }
    }
    
    public void GetCard(int cardValue)
    {
        _playerCards.Add(cardValue);

        if (cardValue == 1)
        {
            // TODO: Ace Protocol
        }
    }

    public bool CanAcceptCard()
    {
        if (_cardPositions.Count == _playerCards.Count) return false;
        if (GetHandValue() >= 21) return false;
        
        return true;
    }

    private int GetHandValue()
    {
        int returnValue = 0;
        
        foreach (int cardValue in _playerCards)
        {
            returnValue += cardValue;
        }
        
        return returnValue;
    }
    #endregion

    #region Position/Rotation Getters
    public Vector3 GetCurrentCardPosition()
    {
        return _cardPositions[_playerCards.Count ].position;
    }

    public Quaternion GetCurrentCardRotation()
    {
        return _cardPositions[_playerCards.Count].rotation; 
    }

    public Quaternion GetCurrentFaceDownCardRotation()
    {
        return CardPositions.current.GetFaceDownRotation(_playerCards.Count);
    }
    #endregion
}
