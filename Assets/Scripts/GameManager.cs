using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public float RemainingTime => m_remainingTime;
  public float MaxTime => m_maxTime;

  [SerializeField] private float m_employmentWeight;
  [SerializeField] private BossBehaviour m_bossGuy;
  [SerializeField] private float m_sceneRestartTime = 5f;

  [Header("Debug Settings")] [SerializeField]
  private bool m_enableDebug;

  [Tooltip("When Debug option is set, this will be the game time used")] [SerializeField]
  private float m_timeDebug = 180;

  [SerializeField] private GameState m_currentGameState;

  private enum GameState {
    kRunning,
    kLoose,
    kWin,
  }


  private const float m_maxTime = 180; // 3min 
  private float m_remainingTime;

  private bool m_initLoadGame;

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
      case GameState.kRunning:
        if (!CheckEndGameState()) {
          m_remainingTime -= Time.deltaTime;
        }

        break;
      case GameState.kLoose:
        //TODO: loose screen
        if (m_initLoadGame) {
          StartCoroutine(OnGameEnd());
        }

        break;
      case GameState.kWin:
        //TODO: win screen
        if (m_initLoadGame) {
          StartCoroutine(OnGameEnd());
        }

        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  #region GameState

  bool CheckEndGameState() {
    if (RemainingTime <= 0) {
      m_currentGameState = GameState.kLoose;
      m_initLoadGame = true;
      return true;
    }

    if (m_employmentWeight > 1) {
      m_currentGameState = GameState.kWin;
      m_initLoadGame = true;
      return true;
    }

    if (m_employmentWeight < -1) {
      m_currentGameState = GameState.kLoose;
      m_initLoadGame = true;
      return true;
    }

    return false;
  }

  #endregion


  #region GameActions

  private void OnGameStart() {
    m_remainingTime = m_maxTime;
    m_employmentWeight = 0;
#if DEBUG && UNITY_EDITOR
    if (m_enableDebug) {
      m_remainingTime = m_timeDebug;
    }
#endif
    m_currentGameState = GameState.kRunning;
  }

  private IEnumerator OnGameEnd() {
    ExampleAudioTrack.Instance.StopAll();
    m_initLoadGame = false;
    var levelID = SceneManager.GetActiveScene().buildIndex;
    yield return new WaitForSeconds(m_sceneRestartTime);
    SceneManager.LoadScene(levelID);
    OnGameStart();
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