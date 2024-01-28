using System;
using UnityEngine;

public class PlacementArea : MonoBehaviour {
  [SerializeField] private Player m_player;
  [SerializeField] private Transform m_placementTransform;
  private MeshRenderer m_meshRenderer;

  private event Action<ItemEffect> m_modifyScore;

  private void Start() {
    m_meshRenderer = GetComponent<MeshRenderer>();
    m_modifyScore += GameManager.Instance().AddToFunninessWeight;
    
  }

  private void LateUpdate() {
    m_meshRenderer.enabled = m_player.HoldingItem;
  }


  private void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player") && m_player.HoldingItem) {
      var itemRef = m_player.CarriedItem;
      m_player.Drop(m_placementTransform);
      m_modifyScore?.Invoke(itemRef.ItemEffectWeight);
      itemRef.SetDestroy();
      Debug.Log("Drop Item");
    }
  }
}