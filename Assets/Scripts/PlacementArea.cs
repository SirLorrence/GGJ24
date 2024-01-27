using System;
using UnityEngine;

public class PlacementArea : MonoBehaviour {
  [SerializeField] private Player m_player;
  [SerializeField] private Transform m_placementTransform;
  private MeshRenderer m_meshRenderer;

  private void Start() {
    m_meshRenderer = GetComponent<MeshRenderer>();
  }

  private void LateUpdate() {
    m_meshRenderer.enabled = m_player.HoldingItem;
  }


  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      m_player.Drop(m_placementTransform);
      Debug.Log("Drop Item");
    }
  }
}