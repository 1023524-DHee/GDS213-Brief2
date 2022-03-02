using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue;

    private void Awake()
    {
        string[] stringValue = name.Split('_');
        cardValue = int.Parse(stringValue[0]);
        
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
}
