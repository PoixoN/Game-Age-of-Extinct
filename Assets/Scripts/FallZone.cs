using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Tracking wether player felt down
/// </summary>

public class FallZone : MonoBehaviour
{
    [SerializeField] private GameObject panelLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0; //makes stop falling
            panelLost.SetActive(true);
        }
    }
}
