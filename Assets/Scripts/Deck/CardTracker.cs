using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardTracker : MonoBehaviour
{
    public List<GameObject> deckOfCards;

    private List<GameObject> _inUseDeck;
    private int _deckCount;
    
    private void Awake()
    {
        _inUseDeck = new List<GameObject>();
        ShuffleDeck();
    }

    public Card DealCard(Vector3 position, Quaternion rotation)
    {
        GameObject dealtCard = Instantiate(_inUseDeck[0]);
        dealtCard.transform.position = transform.position;
        _inUseDeck.RemoveAt(0);

        Card card = dealtCard.GetComponent<Card>();
        card.CardPlayed(position, rotation);

        return card;
    }

    public Card DealFaceDownCard(Vector3 position, Quaternion rotation)
    {
        GameObject dealtCard = Instantiate(_inUseDeck[0]);
        dealtCard.transform.position = transform.position;
        dealtCard.transform.Rotate(new Vector3(180f, 0f));
        _inUseDeck.RemoveAt(0);

        Card card = dealtCard.GetComponent<Card>();
        card.CardPlayed(position, rotation);
        
        return card;
    }

    private void ShuffleDeck()
    {
        _deckCount++;
        
        if (_inUseDeck.Count == 0)
        {
            foreach (GameObject card in deckOfCards)
            {
                _inUseDeck.Add(card);
            }
        }
        
        for (int i = 0; i < _inUseDeck.Count; i++) 
        {
            GameObject temp = _inUseDeck[i];
            int randomIndex = Random.Range(i, _inUseDeck.Count);
            _inUseDeck[i] = _inUseDeck[randomIndex];
            _inUseDeck[randomIndex] = temp;
        }
    }
}
