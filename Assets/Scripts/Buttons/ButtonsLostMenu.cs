using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Script for actions after clicking on Lost menu buttons and visual appearance 
/// </summary>

public class ButtonsLostMenu : Buttons, IPointerClickHandler
{
    #region Inspector fields
    [SerializeField] private Text coinsText;//=======================
    [SerializeField] private Player player;
    [SerializeField] private GameObject LostMenu;
    [SerializeField] private GameObject PlayerUI;
    #endregion

    private void Start()
    {
        coinsText.text = "You will lose " + player.CollectedDeathcoins.ToString() + " deathcoins";
    }

    #region Method for actions after click 
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;
            case "Home":
                Application.LoadLevel("MainMenu");
                Time.timeScale = 1;
                break;
            case "Continue":
                player.Spawn();
                LostMenu.SetActive(false);
                PlayerUI.SetActive(true);
                Time.timeScale = 1;
                break;
        }
    }
    #endregion
}



