using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform timerUI;

    // Start is called before the first frame update
    void Start()
    {
        timerUI.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float completion = 1 - (/*Replace this number with remaining time*/180f / 180f);
        timerUI.transform.position = new Vector3(completion * -10,0,0);
    }
}