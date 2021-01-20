using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ButtonsCompleteMenu : Buttons, IPointerClickHandler
{
    [SerializeField] private Player player;
    public int nextIdSceneToLoad;
    private bool IsGetx3 = false;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SwitchPause();

        nextIdSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "btnNext":
                SwitchPause();
                SceneManager.LoadScene(nextIdSceneToLoad);
                MakePlayerSave();
                break;
            case "btnHome":
                SwitchPause();
                SceneManager.LoadScene("MainMenu");
                MakePlayerSave();
                break;
            case "btnGetx3":
                SwitchPause();
                IsGetx3 = true;
                //SaveSystem.AddDthcoinsToСommonSum(player.CollectedDeathcoins * 2); //Mult by 2 cause we already have collected dthcoins
                SceneManager.LoadScene("MainMenu");
                MakePlayerSave();
                break;
        }
    }

    private void SwitchPause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }

    private void MakePlayerSave()
    {
        PlayerData playerSave = SaveSystem.PlayerSave;

        int newDeathcoinsSum = playerSave.Deathcoins + player.CollectedDeathcoins * (IsGetx3 ? 3 : 1); //Multiply by 3 the sum of collected deathcoins If player tap to Get x3

        int newLastCompletedLevelId = 
            nextIdSceneToLoad > PlayerPrefs.GetInt("levelAt") //We open next level if nextIdSceneToLoad is more
            ? nextIdSceneToLoad
            : playerSave.LastCompletedLevelId;

        SaveSystem.Instance.SavePlayerData(new PlayerData { 
            Hero = playerSave.Hero,
            Deathcoins = newDeathcoinsSum,
            MaxHealth = playerSave.MaxHealth,
            LastCompletedLevelId = newLastCompletedLevelId
        });
    }
}


