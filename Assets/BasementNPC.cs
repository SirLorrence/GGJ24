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
}
