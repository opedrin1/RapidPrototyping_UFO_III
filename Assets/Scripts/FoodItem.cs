using UnityEngine;

public class FoodItem : MonoBehaviour, IEdible
{
    [SerializeField] private float fillAmount = 10f;

    public float FillAmount => fillAmount;

    public void OnEaten(GameObject eater)
    {
        // Placeholder for eat feedback (sound/particle) once we build the stomach bar system.
        Destroy(gameObject);
    }
}