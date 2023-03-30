using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float time;
    [SerializeField] private Text timerText;

    public float _timeLeft;

    private IEnumerator StartTimer()
    {
        while (true)
        {
            _timeLeft += Time.deltaTime;
            UpdateTimeText();
            yield return null;
        }
    }

    private void Start()
    {
        _timeLeft = time;
        StartCoroutine(StartTimer());
    }

    private void UpdateTimeText()
    {
        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
