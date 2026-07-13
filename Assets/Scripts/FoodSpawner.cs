using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Shared Config")]
    [SerializeField] private TrackConfig trackConfig;

    [Header("Food Prefabs")]
    [SerializeField] private GameObject[] foodPrefabs;

    [Header("Spawn Timing")]
    [SerializeField] private float spawnIntervalMin = 0.8f;
    [SerializeField] private float spawnIntervalMax = 1.8f;

    [Header("Spawn Position")]
    [SerializeField] private float spawnBuffer = 1f;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float minLaneGap = 1.5f;

    private float _timer;
    private float _nextSpawnDelay;
    private float _spawnY;
    private readonly List<int> _validLanesBuffer = new List<int>();

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
            TrySpawnFood();
            SetNextSpawnDelay();
        }
    }

    private void SetNextSpawnDelay()
    {
        _nextSpawnDelay = Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    private void TrySpawnFood()
    {
        if (foodPrefabs == null || foodPrefabs.Length == 0 || trackConfig == null) return;

        _validLanesBuffer.Clear();
        for (int lane = 0; lane < trackConfig.laneCount; lane++)
        {
            if (!ActiveLaneItems.IsBlocked(lane, 1, _spawnY, minLaneGap))
            {
                _validLanesBuffer.Add(lane);
            }
        }

        if (_validLanesBuffer.Count == 0) return; // every lane currently blocked, skip this cycle

        int chosenLane = _validLanesBuffer[Random.Range(0, _validLanesBuffer.Count)];
        GameObject prefab = foodPrefabs[Random.Range(0, foodPrefabs.Length)];
        if (prefab == null) return;

        float x = trackConfig.GetLaneX(chosenLane);
        Vector3 spawnPos = new Vector3(x, _spawnY, 0f);

        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);
        FoodMover mover = obj.GetComponent<FoodMover>();
        if (mover != null) mover.Initialize(chosenLane);
    }
}