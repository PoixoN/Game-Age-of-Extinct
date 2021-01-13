using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SceneManager
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Text deathcoinsTxt;

    private PlayerData playerData;
    private Settings settings;

    void Start()
    {
        playerData = SaveSystem.GetPlayerSave();
        settings = SaveSystem.GetSettingsSave();

        deathcoinsTxt.text = playerData.deathcoins.ToString();
    }
}
