using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] List<AudioClip> synthMusic;
    
    void Start()
    {
        var track = synthMusic[Random.Range(0, synthMusic.Count)];
        ExampleAudioTrack.Instance.Play(track, transform.position);
        StartCoroutine(WaitForSongEnd(track.length));
    }

    IEnumerator WaitForSongEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        ExampleAudioTrack.Instance.Play(synthMusic[Random.Range(0, synthMusic.Count)], transform.position);
    }
}
