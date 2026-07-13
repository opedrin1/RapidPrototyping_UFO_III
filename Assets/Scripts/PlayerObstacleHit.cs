using UnityEngine;

public class PlayerObstacleHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ObstacleMover>() == null) return;

        if (GameOverLoader.Instance != null)
        {
            GameOverLoader.Instance.LoadLoseScene(GameOverReason.HitObstacle);
        }
    }
}