using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
  private PlayerActionsMap m_playerActionMap;

  private Vector2 m_moveVector;

  private Vector2 m_lookVector;

  private Rigidbody m_rigidbody;
  private Camera m_playerCamera;

  [SerializeField] private float m_movementSpeed;
  [SerializeField] private float m_mouseSensitivity;

  float m_cameraYaw = 0f;
  float m_cameraPitch = 0f;

  # region Unity Behaviours

  private void Awake() {
    m_playerActionMap = new PlayerActionsMap();
    m_rigidbody = GetComponent<Rigidbody>();
    m_playerCamera = GetComponentInChildren<Camera>();
    m_playerCamera.transform.localPosition = new Vector3(0, 0.7f, 0);
  }


  private void OnEnable() {
    m_playerActionMap.DefaultMap.Enable();
  }

  private Vector3 m_offset;

  void Start() {
    m_offset = m_playerCamera.transform.position - transform.position;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update() {
    m_lookVector = m_playerActionMap.DefaultMap.LookAction.ReadValue<Vector2>();
    CameraMovement();
  }

  private void FixedUpdate() {
    Movement();
  }

  private void LateUpdate() {
    m_playerCamera.transform.position = transform.position + m_offset;
  }

  # endregion

  #region Controls Behaviours

  private void Movement() {
    m_moveVector = m_playerActionMap.DefaultMap.MoveAction.ReadValue<Vector2>();

    Vector3 vectorPositionX = transform.right * m_moveVector.x;
    Vector3 vectorPositionY = transform.forward * m_moveVector.y;
    if (m_rigidbody != null) {
      m_rigidbody.MovePosition(transform.position +
                               ((vectorPositionX + vectorPositionY).normalized) *
                               (m_movementSpeed * Time.deltaTime));
    }
  }


  private void CameraMovement() {
    m_lookVector = m_playerActionMap.DefaultMap.LookAction.ReadValue<Vector2>();

    m_cameraYaw += m_lookVector.x * m_mouseSensitivity;
    m_cameraPitch += m_lookVector.y * m_mouseSensitivity;

    m_cameraPitch = Mathf.Clamp(m_cameraPitch, -60, 60);

    transform.localEulerAngles = new Vector3(0, m_cameraYaw, 0);
    m_playerCamera.transform.localEulerAngles = new Vector3(-m_cameraPitch, 0);
  }

  #endregion
}