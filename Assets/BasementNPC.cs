using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Timeline;

public class BasementNPC : MonoBehaviour
{
    [SerializeField]
    AudioSource track;

    private void Start()
    {
        track.PlayDelayed(10.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            track.volume = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            track.volume = 0.0f;
        }
    }
}
