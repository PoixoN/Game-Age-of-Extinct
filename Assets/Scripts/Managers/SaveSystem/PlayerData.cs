using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Data consists of hero, sum of deathcoins and opened levels. <br/>
/// OpenedLevels => array index(number of World) TO number of opened levels
/// </summary>
[Serializable]
public class PlayerData
{
    public Hero hero;
    public int deathcoins;
    public int[] openedLevels; //Array index(number of World) TO number of opened levels

    public PlayerData()
    {
        //Default initialization
        hero = Hero.druid;
        deathcoins = 0;
        openedLevels = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; 
    }

    public PlayerData(Hero _hero, int _deathcoins, int[] _openedLevels)
    {
        hero = _hero;
        deathcoins = _deathcoins;
        openedLevels = _openedLevels;
    }
}

