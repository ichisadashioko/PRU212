using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ngăn chặn trùng lặp
        }
    }

    void Start()
    {
        UpdateLevel();
    }

    public void AddExperience(int amount)
    {
        GameState.CURRENT_EXP += amount;
        CheckForLevelUp();
        UpdateInterface();
    }

    void CheckForLevelUp()
    {
        if (GameState.CURRENT_EXP >= GameState.NEXT_LEVEL_EXP_COUNT)
        {
            GameState.CURRENT_LEVEL++;
            UpdateLevel();
            // Hiệu ứng level up
        }
    }

    void UpdateLevel()
    {
        GameState.PREVIOUS_LEVEL_EXP_COUNT = (int)experienceCurve.Evaluate(GameState.CURRENT_LEVEL);
        GameState.NEXT_LEVEL_EXP_COUNT = (int)experienceCurve.Evaluate(GameState.CURRENT_LEVEL + 1);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = GameState.CURRENT_EXP - GameState.PREVIOUS_LEVEL_EXP_COUNT;
        int end = GameState.NEXT_LEVEL_EXP_COUNT - GameState.PREVIOUS_LEVEL_EXP_COUNT;

        levelText.text = GameState.CURRENT_LEVEL.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}
