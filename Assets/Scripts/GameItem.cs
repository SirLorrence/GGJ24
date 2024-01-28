using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

public class GameItem : MonoBehaviour {
  // private LayerMask m_itemMask;
  public ItemEffect ItemEffectWeight => m_itemEffectWeight;

  [Header("Item Settings")] [SerializeField]
  private ItemEffect m_itemEffectWeight;

  private BoxCollider m_collider;

  private void Awake() {
    m_collider = GetComponent<BoxCollider>();
  }

  // void Start() {
  //   m_itemMask = LayerMask.GetMask("Pickable");
  // }

  public void SetCarry(bool value) {
    m_collider.enabled = !value;
  }

  public void SetDestroy() {
    m_collider.enabled = false;
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