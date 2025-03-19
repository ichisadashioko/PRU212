using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;

    int previousLevelsExperience, nextLevelsExperience;


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
        if (GameState.CURRENT_EXP >= nextLevelsExperience)
        {
            GameState.CURRENT_LEVEL++;
            UpdateLevel();
            // Hiệu ứng level up
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(GameState.CURRENT_LEVEL);
        nextLevelsExperience = (int)experienceCurve.Evaluate(GameState.CURRENT_LEVEL + 1);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = GameState.CURRENT_EXP - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        levelText.text = GameState.CURRENT_LEVEL.ToString();
        experienceText.text = start + " exp / " + end + " exp";
        experienceFill.fillAmount = (float)start / (float)end;
    }
}
