using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPositions : MonoBehaviour
{
    public static CardPositions current;

    public List<Transform> dealerPositions;
    public List<Transform> E_PlayerPositions;
    public List<Transform> S_PlayerPositions;
    public List<Transform> W_PlayerPositions;

    private void Awake()
    {
        current = this;
    }

    public Quaternion GetFaceDownRotation(int index)
    {
        Quaternion faceDownRotation = dealerPositions[index].rotation;
        return Quaternion.Euler(-90f, faceDownRotation.eulerAngles.y, faceDownRotation.eulerAngles.z);
    }
}
