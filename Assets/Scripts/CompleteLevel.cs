using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Checks when player OnTriggerEnter and Loads LevelCompleted scene
/// Class for showing Level completed menu after completing level
/// </summary>
public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject LevelCompleteMenu;

    [SerializeField] private Text CommonSumDeathCoins;
    [SerializeField] private Text CollectedDeathCoins;

    bool IsAddDthcoins = false;

    private void Start()
    {
        CommonSumDeathCoins.text = SaveSystem.GetPlayerSave().deathcoins.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !IsAddDthcoins)
        {
            IsAddDthcoins = true;
            CollectedDeathCoins.text = "+ " + player.CollectedDeathcoins;
            SaveSystem.AddDthcoinsToСommonSum(player.CollectedDeathcoins);
            playerUI.SetActive(false);
            LevelCompleteMenu.SetActive(true);
        }
    }
}
