using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrackPool : GenericObjectPool<AudioSource>
{
    public override void AddPoolReference(AudioSource objectToAddReference) => objectToAddReference.GetComponent<IObjectPool>().pool = this;
}

internal interface IObjectPool
{
    public AudioTrackPool pool { get; set; }
}