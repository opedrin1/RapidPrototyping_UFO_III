using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Attach to any object in the Win Scene. Wire your Restart and Quit buttons'
/// OnClick events to these two methods - same pattern as LoseScreenController.
/// </summary>
public class WinScreenController : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Gameplay";

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