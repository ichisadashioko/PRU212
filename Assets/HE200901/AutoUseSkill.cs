using UnityEngine;
public class AutoUseSkill : MonoBehaviour
{
    float last_gun_fired_time_delta = 0;
    static float GUN_LEVEL1_CD = 1f;
    static float GUN_LEVEL2_CD = 0.9f;
    static float GUN_LEVEL3_CD = 0.8f;
    static float GUN_LEVEL4_CD = 0.7f;
    static float GUN_LEVEL5_CD = 0.6f;
    static float GUN_LEVEL6_CD = 0.5f;

    void Update()
    {
        switch (GameState.CURRENT_LEVEL)
        {

        }

        //last_gun_fired_time_delta += Time.deltaTime;
        if(last_gun_fired_time_delta > 1)
        {

        }

    }
}