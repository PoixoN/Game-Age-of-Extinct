using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

/// <summary>
/// Написати самари
/// </summary>
public class SaveSystem : MonoBehaviour
{
    public static PlayerData PlayerSave { get; private set; } = new PlayerData();
    public static Settings SettingsSave { get; private set; } = new Settings();

    public static SaveSystem Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (PlayerPrefs.HasKey("PlayerData"))
            {
                PlayerSave = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("PlayerData"));
            }
        }
        else
        {
            Destroy(this);
        }
    }

    ///// <summary>
    ///// Add collected or earned(ads) deathcoins to common sum of player's deathcoins
    ///// </summary>
    //public static void AddDthcoinsToСommonSum(int deathcoins)
    //{
    //    PlayerSave.Deathcoins += deathcoins;
    //}

    /// <summary>
    /// Set up new value for <c>player.hero</c> and <c>player.deathcoins</c>
    /// </summary>
    public void SavePlayerData(PlayerData data) 
    {
        PlayerSave.Hero = data.Hero;
        PlayerSave.MaxHealth = data.MaxHealth;
        PlayerSave.Deathcoins = data.Deathcoins;
        PlayerSave.LastCompletedLevelId = data.LastCompletedLevelId;

        MakePlayerDataSave();
    }

    private void MakePlayerDataSave()
    {
        PlayerPrefs.SetString("PlayerData", JsonUtility.ToJson(PlayerSave));
        PlayerPrefs.Save();
    }

    private void MakeSettingsSave()
    {
        PlayerPrefs.SetString("Settings", JsonUtility.ToJson(SettingsSave));
        PlayerPrefs.Save();
    }

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
        MakePlayerDataSave();
        MakeSettingsSave();
    }
}
