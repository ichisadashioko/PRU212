using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameWhenDie : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public float update_interval = 1f;
    public float last_update_delta_time = 0;
    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if(last_update_delta_time < update_interval) {return;}
        last_update_delta_time = 0;
        if(GameState.CURRENT_HP <= 0){
            SceneManager.LoadScene("End_Game");
        }
    }
}
