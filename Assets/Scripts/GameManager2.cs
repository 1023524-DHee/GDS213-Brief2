using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 current;
 
    public enum GameState
    {
        Bet_E,
        Bet_S,
        Bet_W,
        Deal,
        P_E,
        P_S,
        P_W,
        P_Dealer,
        End
    }

    public CardPositions cardPositions;
    public CardTracker cardTracker;
    public List<Player> listOfPlayers;

    private GameState _currentGameState;

    private int _numBetsPlaced;
    private int[] _playerBets = new int[3];
    private int[] _playerPayouts = new int[3];
    
    private List<Card> _nCardsList;
    private List<Card> _eCardsList;
    private List<Card> _sCardsList;
    private List<Card> _wCardsList;

    private void Awake()
    {
        current = this;

        _nCardsList = new List<Card>();
        _eCardsList = new List<Card>();
        _sCardsList = new List<Card>();
        _wCardsList = new List<Card>();
    }

    private void Start()
    {
        StartBetPhase();
    }

    #region Bet Phase
    private void StartBetPhase()
    {
        _currentGameState = GameState.Bet_E;
        _numBetsPlaced = 0;
        listOfPlayers[1].BetTurnStart();
    }

    public void PlaceBet(Player.PlayerPositions playerPositions, int amount)
    {
        switch (playerPositions)
        {
            case Player.PlayerPositions.E:
                _playerBets[0] = amount;
                _currentGameState = GameState.Bet_S;
                listOfPlayers[2].BetTurnStart();
                break;
            case Player.PlayerPositions.S:
                _playerBets[1] = amount;
                _currentGameState = GameState.Bet_W;
                listOfPlayers[3].BetTurnStart();
                break;
            case Player.PlayerPositions.W:
                _playerBets[2] = amount;
                break;
        }
        
        _numBetsPlaced++;
        CheckEndBetPhase();
    }
    
    private void CheckEndBetPhase()
    {
        if (_numBetsPlaced < listOfPlayers.Count - 1) return;
        DealPhase();
    }
    #endregion
    
    #region Deal Phase
    private void DealPhase()
    {
        _currentGameState = GameState.Deal;
        StartCoroutine(InitialDeal_Coroutine());
    }

    private IEnumerator InitialDeal_Coroutine()
    {
        for (int ii = 0; ii < 2; ii++)
        {
            foreach (Player player in listOfPlayers)
            {
                if (ii == 1 && player.playerPosition == Player.PlayerPositions.N)
                {
                    DealCardFaceDown();
                }
                else
                {
                    DealCard(player.playerPosition);
                }
                
                yield return new WaitForSeconds(0.25f);
            }
        }
        
        PlayPhase();
    }

    #endregion
    
    #region Play Phase
    private void PlayPhase()
    {
        if(_currentGameState == GameState.Deal) _currentGameState = GameState.P_E;

        switch (_currentGameState)
        {
            case GameState.P_E:
                listOfPlayers[1].PlayTurnStart();
                break;
            case GameState.P_S:
                listOfPlayers[2].PlayTurnStart();
                break;
            case GameState.P_W:
                listOfPlayers[3].PlayTurnStart();
                break;
        }
    }
    
    private void PlayerTurnEnd(Player.PlayerPositions player)
    {
        switch (player)
        {
            case Player.PlayerPositions.E:
                _currentGameState = GameState.P_S;
                PlayPhase();
                break;
            case Player.PlayerPositions.S:
                _currentGameState = GameState.P_W;
                PlayPhase();
                break;
            case Player.PlayerPositions.W:
                DealerPlay();
                break;
        }
    }

    private void DealerPlay()
    {
        _nCardsList[1].FlipCard();
        StartCoroutine(DealerAddCard());
    }

    private IEnumerator DealerAddCard()
    {
        yield return new WaitForSeconds(0.75f);
        while (GetHandValues(Player.PlayerPositions.N) < 17)
        {
            DealCard(Player.PlayerPositions.N);
            yield return new WaitForSeconds(0.75f);
        }
        
        EndPhase();
    }
    #endregion
    
    #region End Phase
    private void EndPhase()
    {
        int dealerHandValue = GetHandValues(Player.PlayerPositions.N);
        int eHandValue = GetHandValues(Player.PlayerPositions.E);
        int sHandValue = GetHandValues(Player.PlayerPositions.S);
        int wHandValue = GetHandValues(Player.PlayerPositions.W);

        Debug.Log(dealerHandValue);
        Debug.Log(eHandValue);
        Debug.Log(sHandValue);
        Debug.Log(wHandValue);
        
        if (dealerHandValue <= 21)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                int valueToCheck = 0;
                if(ii == 0) valueToCheck = eHandValue;
                if(ii == 1) valueToCheck = sHandValue;
                if(ii == 2) valueToCheck = wHandValue;

                if (valueToCheck > dealerHandValue) _playerPayouts[ii] = _playerBets[ii] * 2;
                else if (valueToCheck == dealerHandValue) _playerPayouts[ii] = _playerBets[ii];
                else _playerPayouts[ii] = 0;

                if (valueToCheck > 21) _playerPayouts[ii] = 0;
            }
        }
        else
        {
            for (int ii = 0; ii < 3; ii++)
            {
                int valueToCheck = 0;
                if(ii == 0) valueToCheck = eHandValue;
                if(ii == 1) valueToCheck = sHandValue;
                if(ii == 2) valueToCheck = wHandValue;

                if (valueToCheck <= 21) _playerPayouts[ii] = _playerBets[ii] * 2;
                else _playerPayouts[ii] = 0;
            }
        }

        for (int ii = 0; ii < 3; ii++)
        {
            //Debug.Log(_playerPayouts[ii]);
            listOfPlayers[ii + 1].GainMoney(_playerPayouts[ii]);
        }
        
        StartCoroutine(RemoveCards());
    }

    private IEnumerator RemoveCards()
    {
        yield return new WaitForSeconds(5f);
        for (int ii = 0; ii < 4; ii++)
        {
            List<Card> hand = new List<Card>();
            if (ii == 0) hand = _nCardsList;
            if (ii == 1) hand = _eCardsList;
            if (ii == 2) hand = _sCardsList;
            if (ii == 3) hand = _wCardsList;

            foreach (Card card in hand)
            {
                card.RemoveCard();
                yield return new WaitForSeconds(0.2f);
            }
            hand.Clear();
        }
        
        StartBetPhase();
    }
    #endregion
    
    #region Card Dealing Functions
    private void DealCard(Player.PlayerPositions playerPositions)
    {
        Vector3 tempPosition;
        Quaternion tempRotation;
        
        switch (playerPositions)
        {
            case Player.PlayerPositions.N:
                cardPositions.GetPositionDetails(playerPositions, _nCardsList.Count, false, out tempPosition, out tempRotation);
                _nCardsList.Add(cardTracker.DealCard(tempPosition, tempRotation));
                break;
            case Player.PlayerPositions.E:
                cardPositions.GetPositionDetails(playerPositions, _eCardsList.Count, false, out tempPosition, out tempRotation);
                _eCardsList.Add(cardTracker.DealCard(tempPosition, tempRotation));
                break;
            case Player.PlayerPositions.S:
                cardPositions.GetPositionDetails(playerPositions, _sCardsList.Count, false, out tempPosition, out tempRotation);
                _sCardsList.Add(cardTracker.DealCard(tempPosition, tempRotation));
                break;
            case Player.PlayerPositions.W:
                cardPositions.GetPositionDetails(playerPositions, _wCardsList.Count, false, out tempPosition, out tempRotation);
                _wCardsList.Add(cardTracker.DealCard(tempPosition, tempRotation));
                break;
        }
    }

    private void DealCardFaceDown()
    {
        cardPositions.GetPositionDetails(Player.PlayerPositions.N, _nCardsList.Count, true, out var tempPosition, out var tempRotation);
        _nCardsList.Add(cardTracker.DealFaceDownCard(tempPosition, tempRotation));
    }
    #endregion
    
    #region Helper Functions

    private int GetHandValues(Player.PlayerPositions playerPositions)
    {
        List<Card> handToCheck = new List<Card>();

        switch (playerPositions)
        {
            case Player.PlayerPositions.N:
                handToCheck = _nCardsList;
                break;
            case Player.PlayerPositions.E:
                handToCheck = _eCardsList;
                break;
            case Player.PlayerPositions.S:
                handToCheck = _sCardsList;
                break;
            case Player.PlayerPositions.W:
                handToCheck = _wCardsList;
                break;
        }

        return handToCheck.Sum(card => card.cardValue);
    }
    
    private bool CheckCanGetCard(Player.PlayerPositions playerPositions)
    {
        if (GetHandValues(playerPositions) < 21) return true;

        return false;
    }

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
    #endregion
    
    #region Player Functions

    public void PlayerHit(Player.PlayerPositions playerPositions)
    {
        if(CheckCanGetCard(playerPositions)) DealCard(playerPositions);
    }

    public void PlayerStand(Player.PlayerPositions playerPositions)
    {
        PlayerTurnEnd(playerPositions);
    }
    
    #endregion
}
