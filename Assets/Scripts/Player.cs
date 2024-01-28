using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;

public class Player : MonoBehaviour {
  public bool HoldingItem => m_holdingItem;
  public GameItem CarriedItem => m_carriedItem;


  [SerializeField] private float m_rayDistance;
  [SerializeField] private Transform m_handTransform;

  [Header("Movement Settings")] [SerializeField]
  private float m_movementSpeed = 5.0f;

  [Range(0, 10f)] [SerializeField] private float m_lookSensitivity = 5.0f;

  [Header("Camera Settings")] [SerializeField]
  private float m_cameraTiltSmooth;

  private PlayerInput m_playerInput;
  private PlayerActionsMap m_playerActionMap;

  private Vector2 m_moveVector;
  private Vector2 m_lookVector;
  private Vector3 m_offset;

  private Rigidbody m_rigidbody;
  private Camera m_playerCamera;

  GameItem m_carriedItem;


  float m_cameraYaw = 0f;
  float m_cameraPitch = 0f;

  private int m_pickupLayer;
  private int m_interactLayer;

  private bool m_canInteract;

  private bool m_holdingItem;
  private bool m_canPickUp;


  # region Unity Behaviours

  private void Awake() {
    m_playerActionMap = new PlayerActionsMap();
    m_rigidbody = GetComponent<Rigidbody>();
    m_playerCamera = GetComponentInChildren<Camera>();
    m_playerCamera.transform.localPosition = new Vector3(0, transform.localScale.y - .2f, 0);
    m_playerInput = GetComponent<PlayerInput>();

    m_pickupLayer = 1 << LayerMask.NameToLayer("Pickable");
    m_interactLayer = 1 << LayerMask.NameToLayer("Interactable");
  }


  private void OnEnable() {
    m_playerInput.actions = m_playerActionMap.asset;
    m_playerActionMap.DefaultMap.Enable();

    m_playerActionMap.DefaultMap.Interact.performed += Interact;
    m_playerActionMap.DefaultMap.Quit.performed += Quit;
  }


  void Start() {
    m_offset = m_playerCamera.transform.position - transform.position;
    Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update() {
    CameraMovement();
    WorldIntractable();
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

    // body (head) tilt
    Vector3 tiltDirection = new Vector3(m_moveVector.y, 0, -m_moveVector.x);
    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(tiltDirection),
      m_cameraTiltSmooth * Time.deltaTime);
  }

  private void CameraMovement() {
    m_lookVector = m_playerActionMap.DefaultMap.LookAction.ReadValue<Vector2>();

    m_cameraYaw += m_lookVector.x * (m_lookSensitivity * .1f) * .5f;
    m_cameraPitch += m_lookVector.y * (m_lookSensitivity * .1f) * .5f;

    m_cameraPitch = Mathf.Clamp(m_cameraPitch, -60, 60);

    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, m_cameraYaw, transform.localEulerAngles.z);
    m_playerCamera.transform.localEulerAngles = new Vector3(-m_cameraPitch, 0);
  }

  #endregion


  #region Player Actions

  private void WorldIntractable() {
    RaycastHit raycastHit;
    m_canInteract = false;
    m_canPickUp = false;
    if (Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out raycastHit,
          m_rayDistance, m_pickupLayer | m_interactLayer)) {
      var objectMask = 1 << raycastHit.transform.gameObject.layer;
      if ((objectMask & m_pickupLayer) == m_pickupLayer) {
        if (!m_holdingItem) {
          Debug.Log("Pick Up?");
          m_canPickUp = true;
        }
      }

      if ((objectMask & m_interactLayer) == m_interactLayer) {
        Debug.Log("Interact?");
        m_canInteract = true;
      }
    }
  }

  private void Interact(InputAction.CallbackContext context) {
    if (m_canPickUp && !m_holdingItem) {
      PickUp();
    }

    if (m_canInteract && !m_holdingItem) {
      ObjectInteraction();
    }
  }

  private void ObjectInteraction() {
    RaycastHit hitout;
    if (Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out hitout, m_rayDistance,
          m_interactLayer)) {
      var objEvent = hitout.transform.gameObject.GetComponent<IInteractionEvent>();
      if (!objEvent.HasPlayed) {
        objEvent.PlayEvent();
      }
    }
  }

  private void PickUp() {
    RaycastHit hitout;
    if (Physics.Raycast(m_playerCamera.transform.position, m_playerCamera.transform.forward, out hitout, m_rayDistance,
          m_pickupLayer)) {
      m_carriedItem = hitout.transform.gameObject.GetComponent<GameItem>();
      m_carriedItem.gameObject.transform.parent = m_handTransform;
      m_carriedItem.gameObject.transform.localPosition = Vector3.zero;
      m_carriedItem.gameObject.transform.localRotation = quaternion.identity;
      m_carriedItem.SetCarry(true);
      m_canPickUp = false;
      m_holdingItem = true;
    }
  }

  public void Drop(Transform placementTransform) {
    m_carriedItem.transform.SetParent(placementTransform);
    m_carriedItem.transform.localPosition = Vector3.zero;
    m_carriedItem.transform.localRotation = quaternion.identity;

    m_holdingItem = false;
    m_carriedItem.SetCarry(false);
  }

  private void Quit(InputAction.CallbackContext context) {
    Application.Quit(0);
  }

  #endregion


#if DEBUG || UNITY_EDITOR
  private void OnDrawGizmos() {
    if (m_playerCamera != null) {
      Debug.DrawRay(m_playerCamera.transform.position, m_playerCamera.transform.forward * m_rayDistance, Color.red);
    }
  }
#endif
}