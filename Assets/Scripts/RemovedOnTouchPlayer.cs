using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using TMPro;

public class RemovedOnTouchPlayer : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }
    public float update_interval = 0.1f;
    private float last_update_delta_time = 0;
    public float melee_damage_deal_time = 0;
    public float melee_damage_deal_interval = 1f;

    public bool collide_with_player()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            Vector3 player_position = player.transform.position;
            Vector3 player_size = player.GetComponent<SpriteRenderer>().bounds.size;

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
                return true;
            }
        }

        return false;
    }
    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time > update_interval)
        {
            last_update_delta_time = 0;
            if (collide_with_player())
            {
                // ObjectPoolManager.ReturnGameObjectToPool(gameObject);
                // GameState.CURRENT_ACTIVE_ENEMIES_COUNT -= 1;
                float current_time = Time.time;
                float melee_damage_deal_delta_time = current_time - melee_damage_deal_time;
                if (melee_damage_deal_delta_time > melee_damage_deal_interval)
                {
                    melee_damage_deal_time = current_time;
                    int _damage = 1;
                    GameState.CURRENT_HP -= _damage;
                    GameObject damage_popup_prefab = Resources.Load<GameObject>("damage_popup");
                    if (damage_popup_prefab != null)
                    {
                        var _damage_popup = ObjectPoolManager.SpawnNewTextGameObject(damage_popup_prefab, player.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Text);
                        if (_damage_popup != null)
                        {
                            var _tmp = _damage_popup.GetComponent<TextMeshPro>();
                            if (_tmp != null)
                            {
                                _tmp.text = $"-{_damage}";
                                _tmp.color = new UnityEngine.Color(1f, 0, 0);
                                _tmp.faceColor = new UnityEngine.Color(1f, 0, 0);
                            }
                            var _fade = _damage_popup.GetComponent<DamagePopupFade>();
                            if (_fade != null)
                            {
                                _fade.created_time = Time.time;
                            }
                        }
                    }
                }
            }
        }
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
