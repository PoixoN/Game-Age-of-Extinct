using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// Checks when player OnTriggerEnter and Loads LevelCompleted scene
/// Class for showing Level completed menu after completing level
/// </summary>
public class LevelCompleteZone : MonoBehaviour
{
    [Inject] private Player player;

    [HideInInspector] public int nextIdSceneToLoad;
    private bool IsGetx3 = false;

    private void Start()
    {
        nextIdSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MakePlayerSave();
            GameController.CompleteLevelEvent?.Invoke();
            Time.timeScale = 0;
        }
    }

    private void MakePlayerSave()///////////////////////////////////
    {
        PlayerData playerSave = SaveSystem.PlayerSave;

        int newDeathcoinsSum = playerSave.Deathcoins + player.CollectedDeathcoins * (IsGetx3 ? 3 : 1); //Multiply by 3 the sum of collected deathcoins If player tap to Get x3

        int newLastCompletedLevelId =
            nextIdSceneToLoad > PlayerPrefs.GetInt("levelAt") //We open next level if nextIdSceneToLoad is more
            ? nextIdSceneToLoad
            : playerSave.LastCompletedLevelId;

        SaveSystem.Instance.SavePlayerData(new PlayerData
        {
            Hero = playerSave.Hero,
            Deathcoins = newDeathcoinsSum,
            MaxHealth = playerSave.MaxHealth,
            LastCompletedLevelId = newLastCompletedLevelId
        });
    }
}
