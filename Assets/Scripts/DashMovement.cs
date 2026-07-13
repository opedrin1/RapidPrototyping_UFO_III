using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class LaneDashMovement : MonoBehaviour
{
    [Header("Lane Setup")]
    [SerializeField] private TrackConfig trackConfig;

    [Header("Dash Feel")]
    [SerializeField] private float dashSpeed = 12f;

    private Rigidbody2D _rb;
    private int _currentLane;
    private float _targetX;
    private int _pendingDirection; // set in Update, consumed in FixedUpdate

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.bodyType = RigidbodyType2D.Kinematic; // no physics forces, clean lane snapping + trigger collisions

        // With an odd laneCount (like 5) this lands exactly on the true middle lane.
        _currentLane = trackConfig.laneCount / 2;
        _targetX = GetLaneX(_currentLane);
        transform.position = new Vector2(_targetX, transform.position.y);
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (_pendingDirection != 0)
        {
            TryChangeLane(_pendingDirection);
            _pendingDirection = 0;
        }

        MoveTowardsTarget();
    }

    private void HandleInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        if (keyboard.aKey.wasPressedThisFrame || keyboard.leftArrowKey.wasPressedThisFrame)
        {
            _pendingDirection = -1;
        }
        else if (keyboard.dKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
        {
            _pendingDirection = 1;
        }
    }

    private void TryChangeLane(int direction)
    {
        int newLane = Mathf.Clamp(_currentLane + direction, 0, trackConfig.laneCount - 1);
        if (newLane == _currentLane) return;

        _currentLane = newLane;
        _targetX = GetLaneX(_currentLane);
    }

    private void MoveTowardsTarget()
    {
        float newX = Mathf.MoveTowards(_rb.position.x, _targetX, dashSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(new Vector2(newX, _rb.position.y));
    }

    private float GetLaneX(int laneIndex) => trackConfig.GetLaneX(laneIndex);

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Draws a vertical line through every lane center to visually confirm the TrackConfig values line up with the track art.
        if (trackConfig == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < trackConfig.laneCount; i++)
        {
            float x = GetLaneX(i);
            Vector3 pos = new Vector3(x, transform.position.y, 0f);
            Gizmos.DrawLine(pos + Vector3.up * 5f, pos + Vector3.down * 5f);
        }
    }
#endif
}