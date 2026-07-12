using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class DashMovement : MonoBehaviour
{
    [Header("Lane Setup")]
    [SerializeField] private int laneCount = 5;
    
    [SerializeField] private float laneWidth = 0.11f;
    
    [SerializeField] private float trackCenterX = 0f;

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
        _currentLane = laneCount / 2;
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
        int newLane = Mathf.Clamp(_currentLane + direction, 0, laneCount - 1);
        if (newLane == _currentLane) return; // already at the edge lane, ignore

        _currentLane = newLane;
        _targetX = GetLaneX(_currentLane);
    }

    private void MoveTowardsTarget()
    {
        float newX = Mathf.MoveTowards(_rb.position.x, _targetX, dashSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(new Vector2(newX, _rb.position.y));
    }

    private float GetLaneX(int laneIndex)
    {
        // lane 0 = leftmost lane, laneCount-1 = rightmost lane
        float offsetFromCenter = (laneIndex - (laneCount - 1) / 2f) * laneWidth;
        return trackCenterX + offsetFromCenter;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Draws a vertical line through every lane center so you can visually confirm laneWidth/trackCenterX line up with your track art.
        Gizmos.color = Color.yellow;
        for (int i = 0; i < laneCount; i++)
        {
            float x = GetLaneX(i);
            Vector3 pos = new Vector3(x, transform.position.y, 0f);
            Gizmos.DrawLine(pos + Vector3.up * 5f, pos + Vector3.down * 5f);
        }
    }
#endif
}