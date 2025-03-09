using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

public class TmpDeathOnTouchingBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public float update_interval = 0.1f;
    private float last_update_delta_time = 0;

    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time > update_interval)
        {
            last_update_delta_time = 0;

            var bullet_list = GameObject.FindGameObjectsWithTag("bullet");
            for (int i = 0; i < bullet_list.Length; i++)
            {
                GameObject bullet = bullet_list[i];
                Vector3 player_position = bullet.transform.position;
                Vector3 player_size = bullet.GetComponent<SpriteRenderer>().bounds.size;

                float player_left_x = player_position.x - player_size.x / 2;
                float player_right_x = player_position.x + player_size.x / 2;
                float player_top_y = player_position.y - player_size.y / 2;
                float player_bottom_y = player_position.y + player_size.y / 2;


                Vector3 gameObject_position = gameObject.transform.position;
                Vector3 gameObject_size = gameObject.GetComponent<SpriteRenderer>().bounds.size;

                float gameObject_left_x = gameObject_position.x - gameObject_size.x / 2;
                float gameObject_right_x = gameObject_position.x + gameObject_size.x / 2;
                float gameObject_top_y = gameObject_position.y - gameObject_size.y / 2;
                float gameObject_bottom_y = gameObject_position.y + gameObject_size.y / 2;

                Rect player_rect = new Rect(player_left_x, player_top_y, player_size.x, player_size.y);
                //player_rect.
                //Rect.Inter
                int int_scale = 1000;
                Rectangle player_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
                //Rectangle gameObject_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
                Rectangle gameObject_rectangle = new Rectangle((int)(gameObject_left_x * int_scale), (int)(gameObject_top_y * int_scale), (int)(gameObject_size.x * int_scale), (int)(gameObject_size.y * int_scale));
                if (!Rectangle.Intersect(player_rectangle, gameObject_rectangle).IsEmpty)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(gameObject);
                    SpawnEnemies.current_spawned_obj_count -= 1;
                    Die();
                }
            }
        }
    }
    private void Die()
    {
        DropExp();
        ObjectPoolManager.ReturnGameObjectToPool(gameObject);

    }

    private GameObject expPrefab;

    private void DropExp()
    {
        if (expPrefab == null)
        {
            expPrefab = Resources.Load<GameObject>("duc_exp_prefab");
        }

        if (expPrefab != null)
        {
            Vector3 dropPosition = transform.position + new Vector3(0, 0.5f, 0);
            // Instantiate(expPrefab, dropPosition, Quaternion.identity);
            ObjectPoolManager.SpawnNewGameObject(expPrefab, dropPosition, Quaternion.identity);
        }
    }
}
