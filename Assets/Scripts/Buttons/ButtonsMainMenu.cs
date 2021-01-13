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
                break;
            case "TapToStart":
                int sceneToLoad = PlayerPrefs.GetInt("levelAt");
                SceneManager.LoadScene(sceneToLoad);
                if (sceneToLoad == 12)
                {
                    SceneManager.LoadScene(sceneToLoad - 1);
                }
                break;
            case "Back":
                CommingSoon.SetActive(false);
                break;
        }
    }
}
