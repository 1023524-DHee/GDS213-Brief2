using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerPositions
    {
        E,
        S,
        W
    }

    public PlayerPositions playerPosition;

    private List<GameObject> _playerCards;
    private List<Vector3> _cardPositions;
    private List<Transform> _cardPositionOrigin;

    public GameObject cardPositionParent;

    private void Start()
    {
        GetCardPositions();
    }

    private void GetCardPositions()
    {
        switch(playerPosition)
        {
            case PlayerPositions.E:
                _cardPositionOrigin = CardPositions.current.E_PlayerPositions;
                break;
            case PlayerPositions.S:
                _cardPositionOrigin = CardPositions.current.S_PlayerPositions;
                break;
            case PlayerPositions.W:
                _cardPositionOrigin = CardPositions.current.W_PlayerPositions;
                break;
        }
    }

    private void GetCard(GameObject receivedCard)
    {
        _playerCards.Add(receivedCard);
    }

    private void GenerateCardPositions(GameObject receivedCard)
    {
        
    }
}
