using UnityEngine;

public class FoodMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    
    [SerializeField] private float destroyBuffer = 1f;

    private float _destroyY;
    
    public void Initialize(int lane)
    {
        ActiveLaneItems.Register(new ActiveLaneItems.Item
        {
            StartLane = lane,
            WidthInLanes = 1,
            ItemTransform = transform
        });
    }

    private void Start()
    {
        Camera cam = Camera.main;
        float bottomEdge = cam.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).y;
        _destroyY = bottomEdge - destroyBuffer;
    }

    private void Update()
    {
        float speed = StomachController.Instance != null ? StomachController.Instance.CurrentWorldSpeed : moveSpeed;
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < _destroyY)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        ActiveLaneItems.Unregister(transform);
    }
}