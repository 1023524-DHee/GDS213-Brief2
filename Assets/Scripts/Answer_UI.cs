using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Answer_UI : MonoBehaviour
{
    public GameObject answerUI;
    public TMP_Text textField;

    private int _currentValue;
    
    public void ShowUI()
    {
        _currentValue = 0;
        textField.text = "" + _currentValue;
        answerUI.SetActive(true);
    }

    public void AddValue()
    {
        _currentValue++;
        textField.text = "" + _currentValue;
    }

    public void MinusValue()
    {
        _currentValue--;
        textField.text = "" + _currentValue;
    }
    
    public void SubmitAnswer()
    {
        answerUI.SetActive(false);
        GameManager2.current.CardCountEnd(_currentValue);
    }
}
