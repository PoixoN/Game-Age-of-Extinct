using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonsMainMenu : Buttons, IPointerClickHandler
{
    [SerializeField] private GameObject CommingSoon;
    public void OnPointerClick(PointerEventData eventData)
    {

        switch (gameObject.name)
        {
            case "Share":
                CommingSoon.SetActive(true);
                break;
            case "Settings":
                Application.LoadLevel("Settings");
                break;
            case "Levels":
                Application.LoadLevel("LevelsMenu");
                break;
            case "Hero":
                CommingSoon.SetActive(true);
                break;
            case "TapToStart":
                int sceneToLoad = SaveSystem.PlayerSave.LastCompletedLevelId;
                if (sceneToLoad > 12)
                    SceneManager.LoadScene(11);
                else
                    SceneManager.LoadScene(sceneToLoad);
                break;
            case "Back":
                CommingSoon.SetActive(false);
                break;
        }
    }
}
