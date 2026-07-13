using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StomachBarUI : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [Header("Optional: tint the fill by phase")]
    [SerializeField] private Image fillImage;
    [SerializeField] private Color skinnyColor = new Color(0.85f, 0.2f, 0.2f);
    [SerializeField] private Color fitColor = new Color(0.3f, 0.85f, 0.3f);
    [SerializeField] private Color overweightColor = new Color(1f, 0.55f, 0.15f);
    [SerializeField] private Color obeseColor = new Color(1f, 0.85f, 0.2f);

    [Header("Percentage Label")]
    [SerializeField] private TMP_Text percentageTextTMP;

    private void Awake()
    {
        if (slider == null) slider = GetComponent<Slider>();

        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 100f;
            slider.interactable = false; // display-only, not player-draggable
        }
    }

    private void Update()
    {
        if (StomachController.Instance == null || slider == null) return;

        float value = StomachController.Instance.StomachValue;
        slider.value = value;

        if (fillImage != null)
        {
            fillImage.color = GetColorForPhase(StomachController.Instance.CurrentPhase);
        }

        string label = Mathf.RoundToInt(value) + "%";
        if (percentageTextTMP != null) percentageTextTMP.text = label;
    }

    private Color GetColorForPhase(BodyPhase phase)
    {
        switch (phase)
        {
            case BodyPhase.Skinny: return skinnyColor;
            case BodyPhase.Fit: return fitColor;
            case BodyPhase.Overweight: return overweightColor;
            case BodyPhase.Obese: return obeseColor;
            default: return Color.white;
        }
    }
}