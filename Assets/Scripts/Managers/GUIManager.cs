using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GUIManager : MonoBehaviour

{
    [SerializeField] private GameObject panelLost;
    [SerializeField] private GameObject PlayerUI;

    void Start()
    {
        Player.Instance.DeathEvent.AddListener(ShowMenuLose);
    }

    private void ShowMenuLose()
    {
        Time.timeScale = 0;
        PlayerUI.SetActive(false);
        panelLost.SetActive(true);
    }
}
