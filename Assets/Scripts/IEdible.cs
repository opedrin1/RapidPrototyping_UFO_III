using UnityEngine;

/// <summary>
/// Implemented by anything the player can eat.
/// </summary>
public interface IEdible
{
    // how much this fills the stomach bar when eaten (0-100 scale).
    float FillAmount { get; }

    //called when the player eats this item.
    void OnEaten(GameObject eater);
}