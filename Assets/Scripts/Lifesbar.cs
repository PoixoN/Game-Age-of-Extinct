using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Checking state of lives
/// </summary>

public class Lifesbar : MonoBehaviour
{
    private Player player;
    private Transform[] hearts;

    private void Start()
    {
        player = Player.Instance;
        hearts = new Transform[Player.maxHealth];
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
        Player.Instance.HealthChangeEvent.AddListener(OnPlayerChangeHealth);
    }

    #region Refresh method for updating lives
    public void OnPlayerChangeHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.Health)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
    #endregion
}
