using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  [SerializeField] Image timerUI;
  [SerializeField] Text timerTextUI;
  [SerializeField] Image scoreUI;
  [SerializeField] GameObject[] tutorialUI;

  void Start ()
  {
    StartCoroutine(HideUI());
  }

  // Update is called once per frame
  void Update() {
    float completion = 1 - (GameManager.Instance().RemainingTime / GameManager.Instance().MaxTime);
    timerUI.fillAmount = completion;
    timerTextUI.text = ((int)GameManager.Instance().RemainingTime).ToString();
    scoreUI.fillAmount = (GameManager.Instance().EmploymentWeight + 1) / 2;
  }

    IEnumerator HideUI()
    {
        yield return new WaitForSeconds(10);
        foreach (var tutorial in tutorialUI)
        {
            tutorial.gameObject.SetActive(false);
        }
    }
}