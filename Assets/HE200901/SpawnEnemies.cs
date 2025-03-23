using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public string enemy_prefab_name = "mob";
    public string enemy_prefab_name_2 = "Dark_Mage_mob_prefab";
    public GameObject enemy_prefab_2;

    private GameObject obj_to_spawn_prefab;
    public static int max_spawn_count = 10000;
    public float spawn_interval_secs = 0.5f;
    private float last_spawn_delta_time_secs = 0;

    public Vector3 random_position_out_of_screen()
    {
        float random_number = Random.Range(0, 4);
        float random_position_x = 0;
        float random_position_y = 0;
        if (random_number < 1)
        {
            // top left
            // top -1/4 => 0
            // left -1/4 => 0

            random_position_x = Random.Range(-(Screen.width / 4), 0);
            random_position_y = Random.Range(0 - (Screen.height / 4), 0);
        }
        else if (random_number < 2)
        {
            // top right
            random_position_x = Random.Range(Screen.width, Screen.width + Screen.width / 4);
            random_position_y = Random.Range(0 - (Screen.height / 4), 0);
        }
        else if (random_number < 3)
        {
            // bottom left
            random_position_x = Random.Range(-(Screen.width / 4), 0);
            random_position_y = Random.Range(Screen.height + (Screen.height / 4), 0);
        }
        else
        {
            random_position_x = Random.Range(Screen.width, Screen.width + Screen.width / 4);
            random_position_y = Random.Range(Screen.height + (Screen.height / 4), 0);
        }

        Vector3 random_position_in_game_world = Camera.main.ScreenToWorldPoint(new Vector3(random_position_x, random_position_y, 0));
        return random_position_in_game_world;
    }

    // Update is called once per frame
    void Update()
    {
        last_spawn_delta_time_secs += Time.deltaTime;
        if (last_spawn_delta_time_secs > spawn_interval_secs)
        {
            last_spawn_delta_time_secs = 0;
            if (obj_to_spawn_prefab == null) { obj_to_spawn_prefab = Resources.Load<GameObject>(enemy_prefab_name); }
            if (enemy_prefab_2 == null) { enemy_prefab_2 = Resources.Load<GameObject>(enemy_prefab_name_2); }
            if ((obj_to_spawn_prefab == null) || (enemy_prefab_2 == null)) { return; }

            int number_of_mobs_to_create = 1 + (int)(Mathf.Max(0, GameState.CURRENT_DIFFICULTY) * 5);
            number_of_mobs_to_create = Mathf.Max(0, number_of_mobs_to_create - GameState.CURRENT_ACTIVE_ENEMIES_COUNT);
            for (int i = 0; i < number_of_mobs_to_create; i++)
            {

                GameState.CURRENT_ACTIVE_ENEMIES_COUNT += 1;
                Vector3 spawn_location = random_position_out_of_screen();
                spawn_location.z = 0;
                GameObject clone_obj;
                if ((Random.Range(0f, 10f) < 1f) && (GameState.CURRENT_LEVEL > 5))
                {
                    clone_obj = ObjectPoolManager.SpawnNewGameObject(enemy_prefab_2, spawn_location, Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                    var _tmp = clone_obj.GetComponent<EnemyFollow>();
                    if (_tmp != null)
                    {
                        _tmp.speed = 2;
                    }

                    var hp = clone_obj.GetComponent<HPForEnermy>();
                    if (hp != null)
                    {
                        hp.reset_hp();
                    }

                    hp.HP *= 1.5f;
                }
                else
                {
                    clone_obj = ObjectPoolManager.SpawnNewGameObject(obj_to_spawn_prefab, spawn_location, Quaternion.identity, ObjectPoolManager.PoolType.Enemies);
                    var _tmp = clone_obj.GetComponent<EnemyFollow>();
                    if (_tmp != null)
                    {
                        _tmp.speed = 1;
                    }

                    var hp = clone_obj.GetComponent<HPForEnermy>();
                    if (hp != null)
                    {
                        hp.reset_hp();
                    }
                }
            }

            //if (current_spawned_obj_count < max_spawn_count)
            //{
            //    current_spawned_obj_count += 1;
            //    Vector3 spawn_location = new Vector3(0, 0, 0);

            //    float random_position_x = Random.Range(0, Screen.width);
            //    float random_position_y = Random.Range(0, Screen.height);
            //    Vector3 random_position_in_game_world = Camera.main.ScreenToWorldPoint(new Vector3(random_position_x, random_position_y, 0));
            //    spawn_location = random_position_in_game_world;
            //    spawn_location.z = 0;
            //    GameObject clone_obj = ObjectPoolManager.SpawnNewGameObject(obj_to_spawn_prefab, spawn_location, Quaternion.identity);
            //}
        }
    }
}
