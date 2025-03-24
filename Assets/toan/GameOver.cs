using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class GameOver : MonoBehaviour
{
    private UIDocument _document;
    private Button restart_btn;
    private Button menu_btn;
    void Start()
    {
        _document = GetComponent<UIDocument>();
        restart_btn = _document.rootVisualElement.Q("btn_restart") as Button;
        menu_btn = _document.rootVisualElement.Q("btn_menu") as Button;
        restart_btn.RegisterCallback<ClickEvent>(restart_button_clicked);
        menu_btn.RegisterCallback<ClickEvent>(menu_button_clicked);
    }

    private void restart_button_clicked(ClickEvent evt)
    {
        GameState.reset_game_state();
        SceneManager.LoadScene("SampleScene");
    }
    private void menu_button_clicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Start_Game");
    }

    void Update()
    {

    }
}
