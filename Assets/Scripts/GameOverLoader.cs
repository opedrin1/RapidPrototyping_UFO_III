using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameOverReason
{
    Starved,
    HitObstacle
}

public class GameOverLoader : MonoBehaviour
{
    public static GameOverLoader Instance { get; private set; }
    public static GameOverReason LastReason { get; private set; }
    
    [SerializeField] private string loseSceneName = "GameOver";

    private void Awake()
    {
        Instance = this;
    }
    
    public void LoadLoseScene()
    {
        LoadLoseScene(GameOverReason.Starved);
    }

    public void LoadLoseScene(GameOverReason reason)
    {
        LastReason = reason;
        SceneManager.LoadScene(loseSceneName);
    }
}