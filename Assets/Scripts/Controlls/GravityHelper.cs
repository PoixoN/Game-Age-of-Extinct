using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Для того щоб персонаж не зациплявся за стіни, зразу буде падати
/// </summary>
public class GravityHelper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Скрипт управління персонажом буде відключено
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }
}
