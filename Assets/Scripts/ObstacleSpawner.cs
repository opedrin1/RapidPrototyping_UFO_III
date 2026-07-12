using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ObstacleType
    {
        public GameObject prefab;
        
        [Range(1, 5)] public int widthInLanes = 1;
    }

    [Header("Shared Config")]
    [SerializeField] private TrackConfig trackConfig;

    [Header("Obstacle Prefabs")]
    [SerializeField] private ObstacleType[] obstacleTypes;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnIntervalMin = 1f;
    [SerializeField] private float spawnIntervalMax = 2.5f;

    [Header("Spawn Position")]
    [SerializeField] private float spawnBuffer = 1f;

    private float _timer;
    private float _nextSpawnDelay;
    private float _spawnY;

    private void Start()
    {
        Camera cam = Camera.main;
        float topEdge = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 0f)).y;
        _spawnY = topEdge + spawnBuffer;

        SetNextSpawnDelay();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _nextSpawnDelay)
        {
            _timer = 0f;
            SpawnRandomObstacle();
            SetNextSpawnDelay();
        }
    }

    private void SetNextSpawnDelay()
    {
        _nextSpawnDelay = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    private void SpawnRandomObstacle()
    {
        if (obstacleTypes == null || obstacleTypes.Length == 0 || trackConfig == null) return;

        ObstacleType chosen = obstacleTypes[Random.Range(0, obstacleTypes.Length)];
        if (chosen.prefab == null) return;

        int maxStartLane = trackConfig.MaxStartLane(chosen.widthInLanes);
        int startLane = Random.Range(0, maxStartLane + 1); // inclusive upper bound

        float x = trackConfig.GetGroupCenterX(startLane, chosen.widthInLanes);
        Vector3 spawnPos = new Vector3(x, _spawnY, 0f);

        Instantiate(chosen.prefab, spawnPos, Quaternion.identity);
    }
}