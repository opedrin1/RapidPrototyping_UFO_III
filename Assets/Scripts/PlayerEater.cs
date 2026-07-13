using UnityEngine;

public class PlayerEater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IEdible edible = other.GetComponent<IEdible>();
        if (edible == null) return;

        if (StomachController.Instance != null)
        {
            StomachController.Instance.Eat(edible.FillAmount);
        }

        edible.OnEaten(gameObject);
    }
}