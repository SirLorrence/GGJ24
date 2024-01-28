using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour, IObjectPool
{
    public AudioTrackPool pool { get; set; }
}
