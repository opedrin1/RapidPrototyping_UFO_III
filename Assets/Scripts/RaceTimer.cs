using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceTimer : MonoBehaviour
{
    public static RaceTimer Instance { get; private set; }
    
    [SerializeField] private float raceDuration = 120f;
    
    [SerializeField] private string winSceneName = "WinScene";

    private float _timeRemaining;
    private bool _hasFinished;

    // exposed in case you want a countdown display later, same pattern as StomachController.StomachValue
    public float TimeRemaining => _timeRemaining;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeRemaining = raceDuration;
    }

    private void Update()
    {
        if (_hasFinished) return;

        float paceMultiplier = StomachController.Instance != null ? StomachController.Instance.PaceMultiplier : 1f;
        _timeRemaining -= Time.deltaTime * paceMultiplier;

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _hasFinished = true;
            SceneManager.LoadScene(winSceneName);
        }
    }
}