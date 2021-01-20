using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Data consists of hero, sum of deathcoins and opened levels. <br/>
/// OpenedLevels => array index(number of World) TO number of opened levels <br/>
/// Deathcoins - User's total amount of deathcoins
/// </summary>
[Serializable]
public class PlayerData
{
    public Heroes Hero = Heroes.druid;
    public int MaxHealth = 3;
    public int Deathcoins = 0;
    public int LastCompletedLevelId = 2;
}
