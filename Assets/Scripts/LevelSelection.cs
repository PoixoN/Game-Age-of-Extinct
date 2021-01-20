using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public Button[] buttons;

    void Start()
    {
        int LastCompletedLevelId = SaveSystem.PlayerSave.LastCompletedLevelId; //ставим первый уровень открытым всегда

        for(int i = 0; i < buttons.Length; i++)
        {
            if (i + 2 > LastCompletedLevelId)
                buttons[i].interactable = false; // все остальные уровни закрыты
        }
    }
}
