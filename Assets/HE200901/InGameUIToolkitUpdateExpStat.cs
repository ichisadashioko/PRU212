using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameUIToolkitUpdateExpStat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _document = GetComponent<UIDocument>();
        exp_fill = _document.rootVisualElement.Q("exp_bar_fill");
        current_level_label = _document.rootVisualElement.Q("current_level_label") as Label;
        exp_stat_label = _document.rootVisualElement.Q("exp_stat") as Label;
        add_difficulty = _document.rootVisualElement.Q("speed_up_difficulty") as Button;
        add_difficulty.RegisterCallback<ClickEvent>(add_difficulty_button_clicked);
        difficulty_label = _document.rootVisualElement.Q("current_difficulty_label") as Label;
        menu_game_button = _document.rootVisualElement.Q("menu_game_button") as Button;
        resume_game_button = _document.rootVisualElement.Q("resume_game_button") as Button;
        pause_game_button = _document.rootVisualElement.Q("pause_game_button") as Button;
        restart_game_button = _document.rootVisualElement.Q("restart_game_button") as Button;

        menu_game_button.RegisterCallback<ClickEvent>(menu_game_button_clicked);
        resume_game_button.RegisterCallback<ClickEvent>(toggle_pause_game_button_clicked);
        pause_game_button.RegisterCallback<ClickEvent>(toggle_pause_game_button_clicked);
        restart_game_button.RegisterCallback<ClickEvent>(restart_game_button_clicked);
    }



    private void menu_game_button_clicked(ClickEvent evt)
    {
        SceneManager.LoadScene(0);
    }

    private bool isPaused = false;
    private void toggle_pause_game_button_clicked(ClickEvent evt)
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0;
            pause_game_button.style.display = DisplayStyle.None;
            resume_game_button.style.display = DisplayStyle.Flex;
        }
        else
        {
            Time.timeScale = 1;
            pause_game_button.style.display = DisplayStyle.Flex;
            resume_game_button.style.display = DisplayStyle.None;
        }
    }
    private void restart_game_button_clicked(ClickEvent evt)
    {
        Debug.Log("Restarting game...");
        Time.timeScale = 1;
        GameState.reset_game_state();
        SceneManager.LoadScene(1);
    }

    private void add_difficulty_button_clicked(ClickEvent evt)
    {
        GameState.CURRENT_DIFFICULTY += 10;
    }

    private UIDocument _document;
    private VisualElement exp_fill;
    private Label current_level_label;
    private Label exp_stat_label;
    private Label difficulty_label;
    private Button add_difficulty;
    private Button menu_game_button;
    private Button resume_game_button;
    private Button pause_game_button;
    private Button restart_game_button;
    // Update is called once per frame
    void Update()
    {
        int current_level_exp_count = GameState.CURRENT_EXP - GameState.PREVIOUS_LEVEL_EXP_COUNT;
        int current_level_total_required_exp_count = GameState.NEXT_LEVEL_EXP_COUNT - GameState.PREVIOUS_LEVEL_EXP_COUNT;
        if (current_level_total_required_exp_count < 1)
        {
            current_level_total_required_exp_count = 1;
        }
        if (current_level_label != null)
        {
            current_level_label.text = GameState.CURRENT_LEVEL.ToString();
        }
        if (exp_stat_label != null)
        {
            //exp_stat_label.text = start + " exp / " + end + " exp";
            exp_stat_label.text = $"{current_level_exp_count}/{current_level_total_required_exp_count} EXP";
        }
        if (exp_fill != null)
        {
            float fill_amount = (float)current_level_exp_count / (float)current_level_total_required_exp_count;
            fill_amount *= 100f;
            fill_amount = Mathf.Clamp(fill_amount, 0, 100f);
            exp_fill.style.width = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            exp_fill.style.minWidth = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            exp_fill.style.maxWidth = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            //exp_fill.style.minWidth
            //exp_fill.style.maxWidth
        }
        if(difficulty_label != null)
        {
            difficulty_label.text = $"WAVE {GameState.CURRENT_DIFFICULTY}";
        }
    }
}
