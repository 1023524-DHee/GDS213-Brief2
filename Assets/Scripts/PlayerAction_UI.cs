using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction_UI : MonoBehaviour
{
    public GameObject playerAction_UI;

    private Player _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player_S").GetComponent<Player>();
    }

    public void ShowUI()
    {
        playerAction_UI.SetActive(true);
    }

    public void HideUI()
    {
        playerAction_UI.SetActive(false);
    }
    
    public void Hit()
    {
        _player.Hit();
    }

    public void Stand()
    {
        _player.Stand();
    }
}
