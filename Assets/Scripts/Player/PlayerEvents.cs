using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    public static UnityEvent ChangeHealthEvent;
    public static UnityEvent DeathEvent;
    public static UnityEvent CollectDeathcoinEvent;

    private void Awake()
    {
        ChangeHealthEvent = new UnityEvent();
        DeathEvent = new UnityEvent();
        CollectDeathcoinEvent = new UnityEvent();
    }
}
