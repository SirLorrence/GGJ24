// Author: 2401lucas
// Team: Xero Enthusiasm

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  [SerializeField] Image timerUI;
  [SerializeField] Text timerTextUI;
  [SerializeField] Image scoreUI;
  [SerializeField] GameObject[] tutorialUI;
  [SerializeField] GameObject winCanvas;
  [SerializeField] GameObject loseCanvas;

  void Start ()
  {
    StartCoroutine(HideUI());
    winCanvas.SetActive(false);
    loseCanvas.SetActive(false);
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

    public void OnWin()
    {
        winCanvas.SetActive(true);
    }

    public void OnLose()
    {
        loseCanvas.SetActive(true);
    }
    public void OnReset()
    {
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }
}