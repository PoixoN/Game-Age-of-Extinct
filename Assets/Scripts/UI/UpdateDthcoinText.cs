using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateDthcoinText : MonoBehaviour
{
    private int _collectedDeathcoins = 0; //Collected deathcoins in the current level
    private Text _collectedDeathcoinsTxt;

    private void Start()
    {
        _collectedDeathcoinsTxt = GetComponent<Text>();

        PlayerEvents.CollectDeathcoinEvent.AddListener(OnCollectDeathcoin);
    }

    private void OnCollectDeathcoin()
    {
        _collectedDeathcoins++;
        _collectedDeathcoinsTxt.text = _collectedDeathcoins.ToString();
    }
}
