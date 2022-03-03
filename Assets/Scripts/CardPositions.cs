using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

    public void GetPositionDetails(Player.PlayerPositions playerPositions, int index, bool faceDown, out Vector3 position, out Quaternion rotation)
    {
        position = GetPosition(playerPositions, index);
        rotation = faceDown ? GetFaceDownRotation(playerPositions, index) : GetRotation(playerPositions, index);
    }

    private Vector3 GetPosition(Player.PlayerPositions playerPositions, int index)
    {
        Vector3 position = Vector3.zero;

        switch (playerPositions)
        {
            case Player.PlayerPositions.N:
                position = dealerPositions[index].position;
                break;
            case Player.PlayerPositions.E:
                position = E_PlayerPositions[index].position;
                break;
            case Player.PlayerPositions.S:
                position = S_PlayerPositions[index].position;
                break;
            case Player.PlayerPositions.W:
                position = W_PlayerPositions[index].position;
                break;
        }

        return position;
    }
    
    private Quaternion GetRotation(Player.PlayerPositions playerPositions, int index)
    {
        Quaternion rotation = Quaternion.identity;
        
        switch (playerPositions)
        {
            case Player.PlayerPositions.N:
                rotation = dealerPositions[index].rotation;
                break;
            case Player.PlayerPositions.E:
                rotation = E_PlayerPositions[index].rotation;
                break;
            case Player.PlayerPositions.S:
                rotation = S_PlayerPositions[index].rotation;
                break;
            case Player.PlayerPositions.W:
                rotation = W_PlayerPositions[index].rotation;
                break;
        }

        return rotation;
    }
    
    private Quaternion GetFaceDownRotation(Player.PlayerPositions playerPositions, int index)
    {
        Quaternion faceDownRotation = Quaternion.identity;
        
        switch (playerPositions)
        {
            case Player.PlayerPositions.N:
                faceDownRotation = dealerPositions[index].rotation;
                break;
            case Player.PlayerPositions.E:
                faceDownRotation = E_PlayerPositions[index].rotation;
                break;
            case Player.PlayerPositions.S:
                faceDownRotation = S_PlayerPositions[index].rotation;
                break;
            case Player.PlayerPositions.W:
                faceDownRotation = W_PlayerPositions[index].rotation;
                break;
        }
        
        return Quaternion.Euler(-90f, faceDownRotation.eulerAngles.y, faceDownRotation.eulerAngles.z);
    }
}
