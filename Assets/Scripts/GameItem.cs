using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameItem : MonoBehaviour {
  private enum ItemEffect {
    kMarjorPositive,
    kPositive,
    kNegative,
    kMarjorPostive,
    kFire,
  }

  // private LayerMask m_itemMask;

  [Header("Item Settings")] [SerializeField]
  private ItemEffect m_itemEffect;

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
}