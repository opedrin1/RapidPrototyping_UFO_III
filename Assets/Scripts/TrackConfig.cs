using UnityEngine;

[CreateAssetMenu(fileName = "TrackConfig", menuName = "Marathon/Track Config")]
public class TrackConfig : ScriptableObject
{
    public int laneCount = 5;
    
    public float laneWidth = 0.11f;
    
    public float trackCenterX = 0f;

    /// <summary>
    /// World X position of the center of a single lane (0 = leftmost).
    /// </summary>
    public float GetLaneX(int laneIndex)
    {
        float offsetFromCenter = (laneIndex - (laneCount - 1) / 2f) * laneWidth;
        return trackCenterX + offsetFromCenter;
    }

    /// <summary>
    /// World X position of the center of a group of adjacent lanes,
    /// for example a 3-lane-wide hurdle starting at lane 1 covers lanes 1-3.
    /// </summary>
    public float GetGroupCenterX(int startLane, int widthInLanes)
    {
        float startX = GetLaneX(startLane);
        float endX = GetLaneX(startLane + widthInLanes - 1);
        return (startX + endX) / 2f;
    }

    /// <summary>
    /// Highest valid start lane for an obstacle of the given width, so it always fits fully within the track 
    /// </summary>
    public int MaxStartLane(int widthInLanes)
    {
        return Mathf.Max(0, laneCount - widthInLanes);
    }
}