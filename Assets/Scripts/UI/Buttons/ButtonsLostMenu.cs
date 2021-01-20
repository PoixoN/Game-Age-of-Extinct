//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

///// <summary>
///// Script for actions after clicking on Lost menu buttons and visual appearance 
///// </summary>

//public class ButtonsLostMenu : Buttons, IPointerClickHandler
//{
//    [SerializeField] private Text coinsText;
//    [SerializeField] private Player player;
//    [SerializeField] private CanvasRenderer LostPanle;
//    [SerializeField] private Canvas PlayerUI;

//    private void Start()
//    {
//        coinsText.text = "You will lose " + player.CollectedDeathcoins.ToString() + " deathcoins";
//    }

//    #region Method for actions after click 
//    public void OnPointerClick(PointerEventData eventData)
//    {
//        switch (gameObject.name)
//        {
//            case "Restart":
//                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//                Time.timeScale = 1;
//                break;
//            case "Home":
//                Application.LoadLevel("MainMenu");
//                Time.timeScale = 1;
//                break;
//            case "Continue":
//                player.Spawn();
//                player.State = PlayerState.idle;
//                player.Health = SaveSystem.PlayerSave.MaxHealth;
//                LostPanle.gameObject.SetActive(false);
//                PlayerUI.gameObject.SetActive(true);
//                Time.timeScale = 1;
//                break;
//        }
//    }
//    #endregion
//}



