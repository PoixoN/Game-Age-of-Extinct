using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class for Pause menu
/// </summary>
public class ButtonsPauseMenu : Buttons, IPointerClickHandler
{
    [SerializeField] private Canvas playerUI;
    [SerializeField] private CanvasRenderer pausePanel;


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "Pause":
                playerUI.gameObject.SetActive(false);
                pausePanel.gameObject.SetActive(true);
                Time.timeScale = 0; //making pause in game
                break;
            case "Restart":
                Time.timeScale = 1; //unmaking pause in game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Home":
                Time.timeScale = 1; //unmaking pause in game
                Application.LoadLevel("MainMenu");
                break;
            case "Continue":
                Time.timeScale = 1; //unmaking pause in game
                playerUI.gameObject.SetActive(true);
                pausePanel.gameObject.SetActive(false);
                break;
        }
    }
}
