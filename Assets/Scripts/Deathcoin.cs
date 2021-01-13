using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathcoin : MonoBehaviour, ICollectable
{
    private AudioSource deathcoinSound;
    private ParticleSystem particleSystem;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        deathcoinSound = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void Colect(Player player)
    {
        StartCoroutine(AnimParticle());
        deathcoinSound.Play();
        ++player.CollectedDeathcoins;
    }

    IEnumerator AnimParticle()
    {
        particleSystem.Play();
        spriteRenderer.enabled = false; //hide deathcoin sprite
        yield return new WaitForSeconds(0.2f);
        particleSystem.Stop();
        yield return new WaitForSeconds(0.27f);
        Destroy(gameObject);
    }
}
