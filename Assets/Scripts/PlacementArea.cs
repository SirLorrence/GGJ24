using System;
using UnityEngine;

public class PlacementArea : MonoBehaviour {
  [SerializeField] private Player m_player;
  [SerializeField] private Transform m_placementTransform;
  private MeshRenderer m_meshRenderer;

  private event Action<float> m_modifyScore;

  private void Start() {
    m_meshRenderer = GetComponent<MeshRenderer>();
    m_modifyScore += GameManager.Instance().AddToEmploymentWeight;
  }

  private void LateUpdate() {
    m_meshRenderer.enabled = m_player.HoldingItem;
  }


  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      var itemRef = m_player.CarriedItem;
      m_player.Drop(m_placementTransform);
      m_modifyScore?.Invoke(itemRef.GetValue());
      Debug.Log("Drop Item");
    }
  }
}