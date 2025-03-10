using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class GameState
{
	public static int CURRENT_LEVEL = 0;
}

public class ExperienceManager : MonoBehaviour
{
	public static ExperienceManager Instance { get; private set; }

	[Header("Experience")]
	[SerializeField] AnimationCurve experienceCurve;

	int totalExperience;
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
		totalExperience += amount;
		CheckForLevelUp();
		UpdateInterface();
	}

	void CheckForLevelUp()
	{
		if (totalExperience >= nextLevelsExperience)
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
		int start = totalExperience - previousLevelsExperience;
		int end = nextLevelsExperience - previousLevelsExperience;

		levelText.text = GameState.CURRENT_LEVEL.ToString();
		experienceText.text = start + " exp / " + end + " exp";
		experienceFill.fillAmount = (float)start / (float)end;
	}
}
