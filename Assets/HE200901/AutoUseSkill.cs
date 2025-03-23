using System;
using System.Collections.Generic;
using UnityEngine;

public class MobStat
{
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }
}

public class GUN_PROP
{
    public float CD { get; set; }
    public float Count { get; set; }

    public float Damage { get; set; }

    public static List<GUN_PROP> PREDEFINED_BY_LEVEL = null;

    public static void INIT_PREDEFINED_BY_LEVEL()
    {
        GUN_PROP tmp_obj = null;
        PREDEFINED_BY_LEVEL = new()
        {
            new GUN_PROP() { CD = 1f, Count = 1, Damage = 1 },
            new GUN_PROP() { CD = 0.9f, Count = 1, Damage = 2 },
            new GUN_PROP() { CD = 0.9f, Count = 2, Damage = 2 },
            new GUN_PROP() { CD = 0.8f, Count = 2, Damage = 2 },
            new GUN_PROP() { CD = 0.7f, Count = 2, Damage = 3 },
            new GUN_PROP() { CD = 0.7f, Count = 3, Damage = 3 },
            new GUN_PROP() { CD = 0.6f, Count = 3, Damage = 3 },
            new GUN_PROP() { CD = 0.5f, Count = 4, Damage = 3 },
            new GUN_PROP() { CD = 0.5f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.5f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.5f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 },
            new GUN_PROP() { CD = 0.4f, Count = 4, Damage = 4 }
        };

        for (int i = 0; i < 40; i++)
        {
            tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
            tmp_obj = new GUN_PROP()
            {
                CD = tmp_obj.CD - 0.1f,
                Count = tmp_obj.Count + 1,
                Damage = tmp_obj.Damage + 1,
            };

            tmp_obj.CD = Math.Max(0.2f, tmp_obj.CD);
            tmp_obj.Count = Math.Min(16, tmp_obj.Count);
            tmp_obj.Damage = Math.Min(8, tmp_obj.Damage);
            PREDEFINED_BY_LEVEL.Add(tmp_obj);
        }
    }
    public static GUN_PROP GetGunPropByLevel(int level)
    {
        if (PREDEFINED_BY_LEVEL == null)
        {
            INIT_PREDEFINED_BY_LEVEL();
        }

        if (PREDEFINED_BY_LEVEL.Count == 0)
        {
            return new GUN_PROP()
            {
                CD = 0.5f,
                Count = 16,
                Damage = 2,
            };
        }

        level = Math.Clamp(level, 0, PREDEFINED_BY_LEVEL.Count - 1);
        return PREDEFINED_BY_LEVEL[level];
    }
}


public class AutoUseSkill : MonoBehaviour
{
    float last_gun_fired_time_delta = 0;

    public float CURRENT_GUN_CD = 1;
    public float CURRENT_SWORD_CD = 1;


    public string bullet_prefab_name = "Star_Wrath_Bullet";
    public GameObject bullet_prefab;
    public GameObject disc_prefab;
    void Update()
    {
        GUN_PROP gun_prop = GUN_PROP.GetGunPropByLevel(GameState.CURRENT_LEVEL);
        CURRENT_GUN_CD = gun_prop.CD;

        SWORD_PROP sword_prop = SWORD_PROP.GetSwordPropByLevel(GameState.CURRENT_LEVEL);
        CURRENT_SWORD_CD = sword_prop.CD;

        float current_time = Time.time;
        if (!GameState.IS_SWORD_ACTIVE)
        {
            if ((current_time - GameState.LAST_SWORD_USE_TIME) > CURRENT_SWORD_CD)
            {
                GameState.IS_SWORD_ACTIVE = true;
                GameState.LAST_SWORD_USE_TIME = current_time;
                int sword_count = sword_prop.Count;
                sword_count = Math.Max(0, sword_count);
                if (sword_count == 0)
                {
                    GameState.IS_SWORD_ACTIVE = false;
                    GameState.LAST_SWORD_USE_TIME = 0;

                }
                else
                {

                    if (disc_prefab == null)
                    {
                        disc_prefab = Resources.Load<GameObject>("Light_Disc_prefab");
                    }

                    if (disc_prefab != null)
                    {
                        float[] starting_angle_arr = UnityGameUtility.calculate_starting_angle_of_rotating_projectiles(sword_count);
                        float rotate_speed = sword_prop.RotationSpeed;

                        for (int i = 0; i < sword_count; i++)
                        {
                            float starting_angle = starting_angle_arr[i];
                            GameObject disk_obj = ObjectPoolManager.SpawnNewGameObject(
                                disc_prefab,
                                new Vector3(0, 0, 0),
                                Quaternion.identity,
                                active: false
                            );

                            var _tmp = disk_obj.GetComponent<RotateAroundPlayerSkill>();
                            if (_tmp != null)
                            {
                                _tmp.orbitSpeed = rotate_speed;
                                _tmp.angle = starting_angle;
                                _tmp.created_time = current_time;
                                if (i == 0)
                                {
                                    _tmp.modify_game_state = true;
                                }
                                else
                                {
                                    _tmp.modify_game_state = false;
                                }
                            }

                            disk_obj.SetActive(true);
                        }
                    }
                }
            }
        }

        last_gun_fired_time_delta += Time.deltaTime;
        if (last_gun_fired_time_delta > CURRENT_GUN_CD)
        {
            last_gun_fired_time_delta = 0;


            if (bullet_prefab == null)
            {
                bullet_prefab = Resources.Load<GameObject>(bullet_prefab_name);
            }

            if (bullet_prefab == null)
            {
                Debug.Log("bullet_prefab == null");
                return;
            }

            for (int i = 0; i < gun_prop.Count; i++)
            {
                GameObject bullet_obj = ObjectPoolManager.SpawnNewGameObject(bullet_prefab, gameObject.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Bullet);

                var rb = bullet_obj.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 attackDirection = Vector2.left;

                    // TODO check facing direction
                    var _tmp = GetComponent<FlipSpriteInMovingDirection>();
                    if (_tmp == null)
                    {
                        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                        {
                            attackDirection = Vector2.left;
                        }
                        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                        {
                            attackDirection = Vector2.right;
                        }
                    }
                    else
                    {
                        if (_tmp.GetCurrentFacingDirection())
                        {
                            attackDirection = Vector2.right;
                        }
                        else
                        {
                            attackDirection = Vector2.left;
                        }
                    }
                    float angleSpread = 10f;
                    var baseDirection = attackDirection;

                    float angleOffset = (i - (gun_prop.Count - 1) / 2f) * angleSpread;

                    // Rotate the base direction by the angle offset
                    float angleInRadians = Mathf.Deg2Rad * angleOffset;  // Convert to radians
                    attackDirection = new Vector2(
                        baseDirection.x * Mathf.Cos(angleInRadians) - baseDirection.y * Mathf.Sin(angleInRadians),
                        baseDirection.x * Mathf.Sin(angleInRadians) + baseDirection.y * Mathf.Cos(angleInRadians)
                    );

                    // TODO change fire speed with weapon level
                    float move_speed = 5f;
                    //rb.linearVelocity = Vector2.left * move_speed;
                    rb.linearVelocity = attackDirection * move_speed;
                }
            }
        }
    }
}
