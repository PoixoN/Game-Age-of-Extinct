using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class UIManager : MonoBehaviour
{
    [Inject] private Player player;
    [Inject] private GameController gameController;

    [SerializeField] private Canvas _playerUI;
    [SerializeField] private CanvasRenderer _gameOverPanel;
    [SerializeField] private CanvasRenderer _pausePanel;
    [SerializeField] private CanvasRenderer _levelCompletedPanel;

    [HideInInspector] public int nextIdSceneToLoad;
    private bool IsGetx3 = false;

    void Start()
    {
        PlayerEvents.DeathEvent.AddListener(ShowGameOverMenu);
        GameController.CompleteLevelEvent.AddListener(HidePlayerUIInterfaces);
        GameController.CompleteLevelEvent.AddListener(ShowLevelCompletedMenu);
    }

    #region Show/Hide functions
    public void ShowPlayerUIInterface()
    {
        _playerUI.gameObject.SetActive(true);
    }

    public void HidePlayerUIInterfaces()
    {
        _playerUI.gameObject.SetActive(false);
    }

    public void ShowGameOverMenu()
    {
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void HideGameOverMenu()
    {
        _gameOverPanel.gameObject.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        _pausePanel.gameObject.SetActive(true);
    }

    public void HidePauseMenu()
    {
        _pausePanel.gameObject.SetActive(false);
    }

    public void ShowLevelCompletedMenu()
    {
        _levelCompletedPanel.gameObject.SetActive(true);
    }

    public void HideLevelCompletedMenu()
    {
        _levelCompletedPanel.gameObject.SetActive(false);
    }
    #endregion Show/Hide functions

    #region OnClicked - functions for buttons
    //PlayerUI button
    public void OnPauseBtnClicked()
    {
        HidePlayerUIInterfaces();
        ShowPauseMenu();
        Time.timeScale = 0;
    }

    //LostPanel button
    public void OnContinueBtnClicked()
    {
        Time.timeScale = 1;
        gameController.SpawnPlayer();
        gameController.ResetPlayer();
        HideGameOverMenu();
        ShowPlayerUIInterface();
    }

    //LevelCompletedPanel button
    public void OnNextBtnClicked()
    {
        Time.timeScale = 1;
        //MakePlayerSave();
        SceneManager.LoadScene(nextIdSceneToLoad);
    }

    //LevelCompletedPanel button
    public void OnGetx3BtnClicked()
    {
        Time.timeScale = 1;
        IsGetx3 = true;
        //SaveSystem.AddDthcoinsToСommonSum(player.CollectedDeathcoins * 2); //Mult by 2 cause we already have collected dthcoins
        //MakePlayerSave();
        SceneManager.LoadScene("MainMenu");
    }

    public void OnHomeBtnClicked()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
    }

    public void OnPlayBtnClicked()
    {
        ShowPlayerUIInterface();
        HidePauseMenu();
        Time.timeScale = 1;
    }

    public void OnRestartBtnClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion OnClicked
}
