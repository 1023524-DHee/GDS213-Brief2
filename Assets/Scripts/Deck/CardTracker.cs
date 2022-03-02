using System;
using System.Collections;
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

    public int DealCard(Vector3 position, Quaternion rotation)
    {
        GameObject dealtCard = Instantiate(deckOfCards[0]);
        dealtCard.transform.position = transform.position;
        deckOfCards.RemoveAt(0);

        Card card = dealtCard.GetComponent<Card>();
        card.CardPlayed(position, rotation);

        return card.cardValue;
    }

    public int DealFaceDownCard(Vector3 position, Quaternion rotation)
    {
        GameObject dealtCard = Instantiate(deckOfCards[0]);
        dealtCard.transform.position = transform.position;
        dealtCard.transform.Rotate(new Vector3(180f, 0f));
        deckOfCards.RemoveAt(0);

        Card card = dealtCard.GetComponent<Card>();
        card.CardPlayed(position, rotation);
        
        return card.cardValue;
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
