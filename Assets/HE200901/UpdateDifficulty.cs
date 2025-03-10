using UnityEngine;

public class UpdateDifficulty : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    float last_update_time_delta = 0f;
    public float update_interval = 60f;
    // Update is called once per frame
    void Update()
    {
        last_update_time_delta += Time.deltaTime;
        if(last_update_time_delta > update_interval)
        {
            last_update_time_delta = 0;
            GameState.CURRENT_DIFFICULTY++;
        }
    }
}
