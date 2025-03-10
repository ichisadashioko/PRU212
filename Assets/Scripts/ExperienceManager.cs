using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
	public static ExperienceManager Instance { get; private set; }

	[Header("Experience")]
	[SerializeField] AnimationCurve experienceCurve;

	int currentLevel, totalExperience;
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
			currentLevel++;
			UpdateLevel();
			// Hiệu ứng level up
		}
	}

	void UpdateLevel()
	{
		previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
		nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
		UpdateInterface();
	}

	void UpdateInterface()
	{
		int start = totalExperience - previousLevelsExperience;
		int end = nextLevelsExperience - previousLevelsExperience;

		levelText.text = currentLevel.ToString();
		experienceText.text = start + " exp / " + end + " exp";
		experienceFill.fillAmount = (float)start / (float)end;
	}
}
