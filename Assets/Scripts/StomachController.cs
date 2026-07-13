using UnityEngine;

public enum BodyPhase
{
    Skinny,     // 0-40
    Fit,        // 41-60
    Overweight, // 61-70
    Obese       // 71-100
}

public class StomachController : MonoBehaviour
{
    public static StomachController Instance { get; private set; }

    [Header("Stomach Bar")]
    [SerializeField, Range(0f, 100f)] private float stomachValue = 50f;
    
    [SerializeField] private float drainPerSecond = 10f;

    [Header("Phase Thresholds (upper bound, inclusive)")]
    [SerializeField] private float skinnyMax = 40f;
    [SerializeField] private float fitMax = 60f;
    [SerializeField] private float overweightMax = 70f;
    // Obese = anything above overweightMax, up to 100.

    [Header("World Speed Per Phase")]
    [SerializeField] private float skinnySpeed = 3.5f;
    [SerializeField] private float fitSpeed = 2f;
    [SerializeField] private float overweightSpeed = 1f;
    [SerializeField] private float obeseSpeed = 0.3f;

    public float StomachValue => stomachValue;
    public BodyPhase CurrentPhase { get; private set; }
    public float CurrentWorldSpeed { get; private set; }

    private void Awake()
    {
        Instance = this;
        RecalculatePhaseAndSpeed();
    }

    private void Update()
    {
        stomachValue = Mathf.Clamp(stomachValue - drainPerSecond * Time.deltaTime, 0f, 100f);
        RecalculatePhaseAndSpeed();
    }

    // call when the player eats something - raises the bar by the food's FillAmount.
    public void Eat(float fillAmount)
    {
        stomachValue = Mathf.Clamp(stomachValue + fillAmount, 0f, 100f);
        RecalculatePhaseAndSpeed();
    }

    private void RecalculatePhaseAndSpeed()
    {
        if (stomachValue <= skinnyMax)
        {
            CurrentPhase = BodyPhase.Skinny;
            CurrentWorldSpeed = skinnySpeed;
        }
        else if (stomachValue <= fitMax)
        {
            CurrentPhase = BodyPhase.Fit;
            CurrentWorldSpeed = fitSpeed;
        }
        else if (stomachValue <= overweightMax)
        {
            CurrentPhase = BodyPhase.Overweight;
            CurrentWorldSpeed = overweightSpeed;
        }
        else
        {
            CurrentPhase = BodyPhase.Obese;
            CurrentWorldSpeed = obeseSpeed;
        }
    }
}