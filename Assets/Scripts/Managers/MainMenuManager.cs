using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//SceneManager
public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Text deathcoinsTxt;

    private PlayerData playerData;

    void Start()
    {
        playerData = SaveSystem.PlayerSave;

        deathcoinsTxt.text = playerData.Deathcoins.ToString();
    }
}
