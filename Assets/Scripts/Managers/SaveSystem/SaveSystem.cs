using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

/// <summary>
/// Написати самари и зробити синглтоном
/// </summary>
public class SaveSystem : MonoBehaviour
{
    [SerializeField] private const int numberOfWorlds = 10;
    
    private static SaveSystem _instance = null; // Экземпляр объекта

    private static PlayerData playerSave = new PlayerData();
    private static Settings settingsSave = new Settings();

    private void Awake()
    {
        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

            if (PlayerPrefs.HasKey("PlayerData"))
            {
                SubPlayerData subPlayerData;
                subPlayerData = JsonUtility.FromJson<SubPlayerData>(PlayerPrefs.GetString("PlayerData"));
                playerSave = ConverteToPlayerData(subPlayerData);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private PlayerData ConverteToPlayerData(SubPlayerData subPlayerData)
    {
        return new PlayerData((Hero)subPlayerData.hero, subPlayerData.deathcoins, subPlayerData.openedLevels);
    }

    #region Make Saves
    /// <summary>
    /// Add collected or earned(ads) deathcoins to common sum of player's deathcoins
    /// </summary>
    public static void AddDthcoinsToСommonSum(int deathcoins)
    {
        playerSave.deathcoins += deathcoins;
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Set up new value for <c>player.hero</c> and <c>player.deathcoins</c>
    /// </summary>
    public static void MakePlayerSave(PlayerData playerData) 
    {
        playerSave.hero = playerData.hero;
        playerSave.deathcoins = playerData.deathcoins;
        PlayerPrefs.Save();
    }

    public static void MakeSettingsSave(Settings settingsData)
    {

    }
    #endregion

    #region Get Saves
    public static PlayerData GetPlayerSave()
    {
        return playerSave;
    }

    public static Settings GetSettingsSave()
    {
        return settingsSave;
    }
    #endregion

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(playerSave));
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(settingsSave));
        PlayerPrefs.Save();
    }
#endif

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(playerSave));
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(settingsSave));
        PlayerPrefs.Save();
    }

    private class SubPlayerData
    {
        public int hero;
        public int deathcoins;
        public int[] openedLevels;
    }
}
