using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Parent class for all buttons <br/>
/// Makes buttons scale in the moment of putting finger/mouse on it
/// </summary>
public class Buttons : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    #region Buttons scale
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 newScale = new Vector3(0.9f, 0.9f, 0.9f);
        transform.localScale = Vector3.Lerp(transform.localScale, newScale, 5f);
        audioSource.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3 newScale = new Vector3(1f, 1f, 1f);

        transform.localScale = Vector3.Lerp(transform.localScale, newScale, 5f);
    }
    #endregion
}
