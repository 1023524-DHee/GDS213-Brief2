using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager current;
    
    public enum GameState
    {
        Bet,
        Deal,
        P_E,
        P_S,
        P_W,
        End
    }
    
    public CardTracker cardTracker;
    public List<Player> listOfPlayers;

    
    private int _numPlayers;
    private int[] _playerBets = new int[3];
    private bool[] _playerHasBet = new bool[3];
    private int[] _payout = new int[3];
    private int _numBetsPlaced;
    
    private GameState _currentGameState;

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        BetPhase();
    }

    #region Bet Phase
    private void BetPhase()
    {
        _currentGameState = GameState.Bet;

        foreach (Player player in listOfPlayers)
        {
            if(player.playerPosition == Player.PlayerPositions.N) continue;
            
            player.BetTurnStart();
        }
    }
    
    public void BetAmount(Player.PlayerPositions playerPositions, int amount)
    {
        switch (playerPositions)
        {
            case Player.PlayerPositions.E:
                _playerBets[0] = amount;
                _playerHasBet[0] = true;
                break;
            case Player.PlayerPositions.S:
                _playerBets[1] = amount;
                _playerHasBet[1] = true;
                break;
            case Player.PlayerPositions.W:
                _playerBets[2] = amount;
                _playerHasBet[2] = true;
                break;
        }

        EndBetPhase();
    }

    private void EndBetPhase()
    {
        foreach (bool placedBet in _playerHasBet)
        {
            if (!placedBet) return;
        }
        
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
                    //DealCardFaceDown(player);
                }
                else
                {
                   // DealCard(player);
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

    public void PlayerTurnEnd(Player.PlayerPositions player)
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
                //DealerPlay();
                break;
        }
    }
    #endregion
    
    #region End Phase
    private void EndPhase()
    {
        _currentGameState = GameState.End;
    }
    #endregion

    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
