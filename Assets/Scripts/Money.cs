using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Money : MonoBehaviour
{
    private TMP_Text _textbox;
    private Player _player;
    
    private void Awake()
    {
        _textbox = GetComponent<TMP_Text>();
        _player = GameObject.FindGameObjectWithTag("Player_S").GetComponent<Player>();
    }

    private void Update()
    {
        _textbox.text = "" + _player.currentMoney;
    }
}
