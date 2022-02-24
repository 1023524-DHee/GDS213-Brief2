using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardTracker : MonoBehaviour
{
    public List<GameObject> deckOfCards;

    private void Awake()
    {
        ShuffleDeck();
    }

    public void DealCard()
    {
        GameObject dealtCard = Instantiate(deckOfCards[0]);
        dealtCard.transform.position = transform.position;
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deckOfCards.Count; i++) 
        {
            GameObject temp = deckOfCards[i];
            int randomIndex = Random.Range(i, deckOfCards.Count);
            deckOfCards[i] = deckOfCards[randomIndex];
            deckOfCards[randomIndex] = temp;
        }
    }
}
