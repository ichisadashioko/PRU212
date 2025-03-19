using System;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class RunGame : MonoBehaviour
{
    private UIDocument _document;
    private Button play_btn;
    void Start()
    {
        _document = GetComponent<UIDocument>();
        play_btn = _document.rootVisualElement.Q("btn_play") as Button;
        play_btn.RegisterCallback<ClickEvent>(play_button_clicked);
    }

    private void play_button_clicked(ClickEvent evt)
    {
        GameState.reset_game_state();
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        
    }
}
