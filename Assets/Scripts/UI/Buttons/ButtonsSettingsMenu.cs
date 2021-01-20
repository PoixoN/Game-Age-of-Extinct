using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Class for actions after clicking on Settings menu buttons
/// </summary>

public class ButtonsSettingsMenu : Buttons, IPointerClickHandler
{
    [SerializeField] private Image imgSoundOnOff;
    [SerializeField] private Sprite SoundOn;
    [SerializeField] private Sprite SoundOff;

    private bool musicOn = true;

    #region Method for actions after click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.name == "SoundOnOff")
        {
            if (musicOn)
            {
                imgSoundOnOff.sprite = SoundOff;
                AudioListener.pause = true;
                musicOn = false;
            }
            else
            {
                imgSoundOnOff.sprite = SoundOn;
                AudioListener.pause = false;
                musicOn = true;
            }
        }

        switch (gameObject.name)
        {
            //case "SoundOn":
            //    break;
            case "RemoveAds":
                break;
            case "Restore":
                break;
            case "Facebook":
                Application.OpenURL("https://www.facebook.com");
                break;
            case "Mail":
                Application.OpenURL("mailto:thegrassmans.inc@gmail.com");
                break;
            case "Instagram":
                Application.OpenURL("https://www.instagram.com");
                break;
            case "Home":
                Application.LoadLevel("MainMenu");
                break;
        }
    }
    #endregion
}
