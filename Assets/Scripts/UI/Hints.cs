using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Hints : MonoBehaviour
{
    [SerializeField] private GameObject HintsMove;
    [SerializeField] private GameObject HintsJump;

    private void Start()
    {
        Invoke("HintsMoveOff", 4f);
    }
    private void HintsMoveOff()
    {
        if (HintsMove.activeSelf)
        {
            HintsMove.SetActive(false);
            HintsJump.SetActive(true);
            Invoke("HintsJumpOff", 4f);
        }
    }
    private void HintsJumpOff()
    {
        HintsJump.SetActive(false);
    }
}

