using System;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] private float m_employmentWeight;
  [SerializeField] private BossBehaviour m_bossGuy;

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
  }

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