using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonsLevelsMenu : Buttons, IPointerClickHandler
{
    public GameObject commingSoon;
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "Return":
                Application.LoadLevel("MainMenu");
                break;
            default:
                {
                    commingSoon.SetActive(true);
                    Invoke("PanelOff", 2.5f);
                }
                break;
        }
    }
    private void PanelOff()
    {
        commingSoon.SetActive(false);
    }
}

