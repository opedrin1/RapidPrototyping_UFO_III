using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks every currently-alive obstacle and food item, keyed by which lanes it
/// occupies. Both ObstacleSpawner and FoodSpawner consult this before spawning,
/// so obstacles and food can never end up overlapping each other
///
/// Relies on the fact that all lane items fall at the same moveSpeed
/// </summary>
public static class ActiveLaneItems
{
    public class Item
    {
        public int StartLane;
        public int WidthInLanes;
        public Transform ItemTransform;
    }

    private static readonly List<Item> items = new List<Item>();

    public static void Register(Item item) => items.Add(item);

    public static void Unregister(Transform t)
    {
        items.RemoveAll(i => i.ItemTransform == t);
    }
    
    public static bool IsBlocked(int startLane, int widthInLanes, float candidateY, float minYGap)
    {
        int candidateEnd = startLane + widthInLanes - 1;

        foreach (var item in items)
        {
            if (item.ItemTransform == null) continue; // destroyed but not yet unregistered

            int itemEnd = item.StartLane + item.WidthInLanes - 1;
            bool laneOverlap = startLane <= itemEnd && item.StartLane <= candidateEnd;
            if (!laneOverlap) continue;

            float yGap = Mathf.Abs(candidateY - item.ItemTransform.position.y);
            if (yGap < minYGap) return true;
        }

        return false;
    }
}