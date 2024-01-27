using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
  [SerializeField] private LayerMask m_itemMask;

  // Start is called before the first frame update
  void Start() {
    m_itemMask = LayerMask.GetMask("Pickable");
  }

  // Update is called once per frame
  void Update() {
  }
}