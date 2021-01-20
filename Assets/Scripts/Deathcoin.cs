using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathcoin : MonoBehaviour, ICollectable
{
    public void Collect(Player player)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        GetComponent<SpriteRenderer>().enabled = false;
        ++player.CollectedDeathcoins;
        Destroy(gameObject, 0.80f);
    }

    public void Artem() { }
}
