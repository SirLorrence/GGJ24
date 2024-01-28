// Author: 2401lucas
// Team: Xero Enthusiasm

using System.Collections;
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
