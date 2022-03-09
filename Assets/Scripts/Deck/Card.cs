using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;

    public int _cardCountValue;
    
    private void Awake()
    {
        string[] stringValue = name.Split('_');
        cardValue = int.Parse(stringValue[0]);

        if (cardValue is >= 2 and <= 6)
        {
            _cardCountValue = 1;
        }
        else if (cardValue is > 6 and <= 9)
        {
            _cardCountValue = 0;
        }
        else if (cardValue is 1 or > 9)
        {
            _cardCountValue = -1;
        }
        
        if(cardValue > 10)
        {
            cardValue = 10;
        }
    }

    public void CardPlayed(Vector3 position, Quaternion rotation)
    {
        StartCoroutine(MoveCardToPosition(position, rotation));
    }
    
    private IEnumerator MoveCardToPosition(Vector3 position, Quaternion rotation)
    {
        float startTime = Time.time;
        Vector3 initialCardPosition = transform.position;
        Quaternion initialCardRotation = transform.rotation;

        while (Time.time < startTime + 0.75f)
        {
            transform.position = Vector3.Lerp(initialCardPosition, position, (Time.time - startTime) / 0.75f);
            transform.rotation = Quaternion.Lerp(initialCardRotation, rotation, (Time.time - startTime) / 0.75f);
            yield return null;
        }

        transform.position = position;
        transform.rotation = rotation;
    }

    public void FlipCard()
    {
        StartCoroutine(FlipCard_Coroutine());
    }

    private IEnumerator FlipCard_Coroutine()
    {
        float startTime = Time.time;
        Quaternion initialCardRotation = transform.rotation;
        Quaternion finalCardRotation = Quaternion.Euler(initialCardRotation.eulerAngles.x + 180,0,0);
        
        while (Time.time < startTime + 0.75f)
        {
            transform.rotation = Quaternion.Lerp(initialCardRotation, finalCardRotation, (Time.time - startTime) / 0.75f);
            yield return null;
        }

        transform.rotation = finalCardRotation;
    }

    public void RemoveCard()
    {
        Destroy(gameObject);
    }
}
