using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ButtonsCompleteMenu : Buttons, IPointerClickHandler
{
    [SerializeField] private Player player;
    public int nextSceneLoad;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        SwitchPause();
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneLoad > PlayerPrefs.GetInt("levelAt")) 
        {
            PlayerPrefs.SetInt("levelAt", nextSceneLoad);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.name)
        {
            case "btnNext":
                SwitchPause();
                SceneManager.LoadScene(nextSceneLoad);
                break;
            case "btnHome":
                SwitchPause();
                SceneManager.LoadScene("MainMenu");
                break;
            case "btnGetx3":
                SwitchPause();
                SaveSystem.AddDthcoinsToСommonSum(player.CollectedDeathcoins * 2); //Mult by 2 cause we already have collected dthcoins
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    private void SwitchPause()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
}


