using UnityEngine;
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
    }

    private UIDocument _document;
    private VisualElement exp_fill;
    private Label current_level_label;
    private Label exp_stat_label;
    // Update is called once per frame
    void Update()
    {
        int start = GameState.CURRENT_EXP - GameState.PREVIOUS_LEVEL_EXP_COUNT;
        int end = GameState.NEXT_LEVEL_EXP_COUNT - GameState.PREVIOUS_LEVEL_EXP_COUNT;
        if(current_level_label != null)
        {
            current_level_label.text= GameState.CURRENT_LEVEL.ToString();
        }
        if(exp_stat_label != null)
        {
            //exp_stat_label.text = start + " exp / " + end + " exp";
            exp_stat_label.text = $"{start}/{end} EXP";
        }
        if(exp_fill != null)
        {
            float fill_amount = (float)start / (float)end;
            fill_amount *= 100f;
            exp_fill.style.width = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            exp_fill.style.minWidth = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            exp_fill.style.maxWidth = new StyleLength(new Length(fill_amount, LengthUnit.Percent));
            //exp_fill.style.minWidth
            //exp_fill.style.maxWidth
        }
    }
}
