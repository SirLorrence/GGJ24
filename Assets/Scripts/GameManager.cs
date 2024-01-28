using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public float RemainingTime => m_remainingTime;
  public float MaxTime => m_maxTime;

  [SerializeField] private float m_employmentWeight;
  [SerializeField] private BossBehaviour m_bossGuy;


  private enum GameState {
    kMenu,
    kRunning,
    kLoose,
    kWin,
  }

  [SerializeField] private GameState m_currentGameState = GameState.kMenu;

  private const float m_maxTime = 180; // 3min 
  private float m_remainingTime;

  private bool m_initGame;

  private static GameManager m_instance;

  public static GameManager Instance() {
    if (m_instance == null) {
      m_instance = GameObject.FindWithTag("GM").gameObject.GetComponent<GameManager>();
      if (m_instance == null) {
        GameObject managerObject = new GameObject("Game Manager");
        m_instance = managerObject.AddComponent<GameManager>();
      }
    }

    return m_instance;
  }


  private void Awake() {
    if (m_instance == null) {
      m_instance = this;
    }

    if (m_instance != this) {
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(m_instance);
  }

  private void Start() {
    m_employmentWeight = 0;


    OnGameStart();
  }

  private void Update() {
    switch (m_currentGameState) {
      case GameState.kMenu:
        break;
      case GameState.kRunning:
        if (!CheckEndGameState()) {
          m_remainingTime -= Time.deltaTime;
        }

        break;
      case GameState.kLoose:
        break;
      case GameState.kWin:
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  #region GameState

  bool CheckEndGameState() {
    if (RemainingTime <= 0) {
      m_currentGameState = GameState.kLoose;
      return true;
    }

    if (m_employmentWeight > 1) {
      m_currentGameState = GameState.kWin;
      return true;
    }

    if (m_employmentWeight < -1) {
      m_currentGameState = GameState.kLoose;
      return true;
    }


    return false;
  }

  #endregion


  #region GameActions

  public void OnGameStart() {
    m_remainingTime = m_maxTime;
    m_currentGameState = GameState.kRunning;
  }

  public void OnGameEnd() {
    m_currentGameState = GameState.kLoose;
  }

  #endregion


  public void AddToFunninessWeight(ItemEffect effectValue) {
    float weightScore = effectValue switch {
      ItemEffect.kVeryFunny => 0.3f,
      ItemEffect.kFunny => 0.2f,
      ItemEffect.kLittleFunny => 0.1f,
      ItemEffect.kNone => 0f,
      ItemEffect.kNotFunnyDidntLaugh => -0.2f,
      _ => throw new ArgumentOutOfRangeException()
    };
    m_bossGuy.Reaction(effectValue);
    m_employmentWeight += weightScore;
  }
}