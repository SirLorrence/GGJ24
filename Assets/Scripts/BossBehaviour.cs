using System;
using UnityEngine;

public class BossBehaviour : MonoBehaviour {
  private Animator m_animator;

  private static readonly int m_LiteTrigger = Animator.StringToHash("LLaugh");
  private static readonly int m_MediumTrigger = Animator.StringToHash("MLaugh");
  private static readonly int m_HighTrigger = Animator.StringToHash("HLaugh");
  private static readonly int m_NegativeTrigger = Animator.StringToHash("NLaugh");


  private void Awake() {
    m_animator = GetComponent<Animator>();
  }

  public void Reaction(ItemEffect inputEffect) {
    if (inputEffect == ItemEffect.kNone) return;
    int triggerID = inputEffect switch {
      ItemEffect.kVeryFunny => m_HighTrigger,
      ItemEffect.kFunny => m_MediumTrigger,
      ItemEffect.kLittleFunny => m_LiteTrigger,
      ItemEffect.kNotFunnyDidntLaugh => m_NegativeTrigger,
      _ => throw new ArgumentOutOfRangeException(nameof(inputEffect), inputEffect, null)
    };
    m_animator.SetTrigger(triggerID);
  }
}