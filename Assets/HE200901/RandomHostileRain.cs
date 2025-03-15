using UnityEngine;

public class RandomHostileRain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public float active_duration = 60f;
    public float start_time = 0;
    public bool active = false;
    public float active_chance = 5f;

    public float deal_damage_interval = 1f;
    public GameObject player;
    public float last_deal_damage_time = 0f;
    public GameObject safe_cover;
    public GameObject safe_portal_prefab;
    public float last_random_number = 0f;

    public float last_spin_chance_to_spawn_time = 0;
    public float spin_to_spawn_interval = 1f;

    // Update is called once per frame
    void Update()
    {
        float current_time = Time.time;
        if (active)
        {

            float delta_time = current_time - start_time;
            if(delta_time > active_duration)
            {
                active = false;
                GameObject safe_portal_obj = GameObject.FindGameObjectWithTag("safe_portal");
                if(safe_portal_obj != null)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(safe_portal_obj);
                }
            }
        }
        else
        {
            if((current_time - last_spin_chance_to_spawn_time) < spin_to_spawn_interval)
            {
                return;
            }

            last_random_number = Random.Range(0f, 100f);
            last_spin_chance_to_spawn_time = current_time;

            if (last_random_number < active_chance)
            {
                start_time = current_time;
                active = true;

                if (safe_portal_prefab == null)
                {
                    safe_portal_prefab = Resources.Load<GameObject>("safe_portal");
                }

                GameObject safe_portal_obj = ObjectPoolManager.SpawnNewGameObject(safe_portal_prefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}
