using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class GameItem : MonoBehaviour {
  public ItemEffect ItemEffectWeight => m_itemEffectWeight;

  [Header("Item Settings")] [SerializeField]
  private ItemEffect m_itemEffectWeight;

  [SerializeField] private AudioClip soundClip;
  [SerializeField] private float clipSound;

  private BoxCollider m_collider;

  private void Awake() {
    m_collider = GetComponent<BoxCollider>();
  }
  public void SetCarry(bool value) {
    m_collider.enabled = !value;
  }

  public void SetDestroy() {
    m_collider.enabled = false;
    if (soundClip != null ) ExampleAudioTrack.Instance.Play(soundClip, worldPos: transform.position, volume: clipSound);
    Destroy(this.gameObject, 5f);
  }
}

public enum ItemEffect {
  kVeryFunny,
  kFunny,
  kLittleFunny,
  kNone,
  kNotFunnyDidntLaugh,
}