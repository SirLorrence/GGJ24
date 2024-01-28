using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRing : MonoBehaviour
{
    [SerializeField]
    float minWait;
    [SerializeField]
    float MaxWait;
    [SerializeField]
    ExampleAudioTrack.ExampleSounds sound;
    [SerializeField]
    float volume;

    void Start()
    {
        StartCoroutine(WaitForPhoneRing());
    }

    IEnumerator WaitForPhoneRing()
    {
        yield return new WaitForSeconds(Random.Range(minWait, MaxWait));
        ExampleAudioTrack.Instance.PlaySound(sound, transform.position);
    }
}
