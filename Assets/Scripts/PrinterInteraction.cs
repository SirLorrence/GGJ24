using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PrinterInteraction : MonoBehaviour, IInteractionEvent {
  [SerializeField] private Transform m_interactionTransform;
  [SerializeField] private Transform m_fileOutput;
  [SerializeField] private Animation m_animation;

  private GameObject m_playerRef;
  private BoxCollider m_boxCollider;

  private void Awake() {
    m_boxCollider = GetComponent<BoxCollider>();
  }

  private void Start() {
    m_interactionTransform.gameObject.SetActive(false);
    m_fileOutput.gameObject.SetActive(false);
    m_boxCollider.isTrigger = true;
    HasPlayed = false;
    m_playerRef = GameManager.Instance().GetPlayer;
  }

  public void PlayEvent() {
    m_playerRef.SetActive(false);
    m_interactionTransform.gameObject.SetActive(true);
    HasPlayed = true;
    StartCoroutine(PlayAnimation());
  }

  IEnumerator PlayAnimation() {
    m_animation.Play();
    yield return new WaitForSeconds(5f);
    m_interactionTransform.gameObject.SetActive(false);
    m_playerRef.SetActive(true);
    m_fileOutput.gameObject.SetActive(true);
  }

  private void OnTriggerExit(Collider other) {
    if (HasPlayed) {
      m_boxCollider.isTrigger = false;
    }
  }

  public bool HasPlayed { get; set; }
}