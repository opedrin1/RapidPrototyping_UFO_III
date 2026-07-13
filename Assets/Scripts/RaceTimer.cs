using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceTimer : MonoBehaviour
{
    public static RaceTimer Instance { get; private set; }
    
    [SerializeField] private float raceDuration = 120f;
    
    [SerializeField] private string winSceneName = "WinScreen";

    private float _timeRemaining;
    private bool _hasFinished;
    
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

        _timeRemaining -= Time.deltaTime;

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _hasFinished = true;
            SceneManager.LoadScene(winSceneName);
        }
    }
}