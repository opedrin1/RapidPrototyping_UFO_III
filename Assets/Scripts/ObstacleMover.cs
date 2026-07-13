using UnityEngine;

/// <summary>
/// Moves straight down and destroys itself once it's fully past the bottom of the camera view.
/// </summary>
public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    [SerializeField] private float destroyBuffer = 1f;

    private float _destroyY;

    private void Start()
    {
        Camera cam = Camera.main;
        float bottomEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        _destroyY = bottomEdge - destroyBuffer;
    }

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y < _destroyY)
        {
            Destroy(gameObject);
        }
    }
}