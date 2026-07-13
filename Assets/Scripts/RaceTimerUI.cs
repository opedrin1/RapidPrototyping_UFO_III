using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceTimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerTextTMP;

    private void Update()
    {
        if (RaceTimer.Instance == null) return;

        float remaining = Mathf.Max(0f, RaceTimer.Instance.TimeRemaining);
        int minutes = Mathf.FloorToInt(remaining / 60f);
        int seconds = Mathf.FloorToInt(remaining % 60f);
        string label = $"{minutes:00}:{seconds:00}";
        
        if (timerTextTMP != null) timerTextTMP.text = label;
    }
}