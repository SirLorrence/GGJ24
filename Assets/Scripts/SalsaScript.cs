using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SalsaScript : MonoBehaviour
{
    [SerializeField]
    AudioSource track;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            track.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            track.Stop();
        }
    }

    private void OnDisable()
    {
        track.Stop();
    }
}
