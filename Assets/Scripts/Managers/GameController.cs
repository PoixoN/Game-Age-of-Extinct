using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;


//Maybe rename as PlayerController
public class GameController : MonoBehaviour
{
    public static UnityEvent CompleteLevelEvent;

    [Inject] Player player;

    [SerializeField] private Transform _spawnPoint;

    private void Awake()
    {
        CompleteLevelEvent = new UnityEvent();
    }

    private void Start()
    {
        SpawnPlayer();
        ResetPlayer();
    }

    /// <summary>
    /// Uses spawn point at the current level 
    /// </summary>
    public void SpawnPlayer()
    {
        player.transform.position = _spawnPoint.position;
    }

    public void ResetPlayer()
    {
        player.State = PlayerState.idle;
        player.Health = SaveSystem.PlayerSave.MaxHealth;
    }
}
