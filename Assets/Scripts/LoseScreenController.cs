using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoseScreenController : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Gameplay";

    [Header("Reason")]
    [SerializeField] private TMP_Text reasonTextTMP;
    [SerializeField] private string starvedMessage = "You starved out there...";
    [SerializeField] private string hitObstacleMessage = "You crashed into a hurdle!";

    private void Start()
    {
        string message = GameOverLoader.LastReason == GameOverReason.Starved
            ? starvedMessage
            : hitObstacleMessage;
        
        if (reasonTextTMP != null) reasonTextTMP.text = message;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}