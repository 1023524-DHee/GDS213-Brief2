using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_UI : MonoBehaviour
{
    public static Result_UI current;
    private Animator _animator;

    private void Awake()
    {
        current = this;
        _animator = GetComponent<Animator>();
    }

    public void CorrectAnimation()
    {
        _animator.SetTrigger("Correct");
    }

    public void WrongAnimation()
    {
        _animator.SetTrigger("Wrong");
    }
}
