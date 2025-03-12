using UnityEngine;
public class AutoUseSkill : MonoBehaviour
{
    float last_gun_fired_time_delta = 0;
    public static readonly float GUN_LEVEL1_CD = 1f;
    public static readonly float GUN_LEVEL2_CD = 0.9f;
    public static readonly float GUN_LEVEL3_CD = 0.8f;
    public static readonly float GUN_LEVEL4_CD = 0.7f;
    public static readonly float GUN_LEVEL5_CD = 0.6f;
    public static readonly float GUN_LEVEL6_CD = 0.5f;
    public static readonly float GUN_LEVEL7_CD = 0.4f;
    public static readonly float GUN_LEVEL8_CD = 0.3f;
    public static readonly float GUN_LEVEL9_CD = 0.2f;
    public static readonly float GUN_LEVEL10_CD = 0.1f;
    public static readonly float GUN_LEVEL11_CD = 0.09f;

    static float GUN_LEVEL_MAX_CD = GUN_LEVEL11_CD;

    public float CURRENT_GUN_CD = GUN_LEVEL1_CD;


    public string bullet_prefab_name = "Star_Wrath_Bullet";
    public GameObject bullet_prefab;
    void Update()
    {
        if (GameState.CURRENT_LEVEL < 1)
        {
            CURRENT_GUN_CD = GUN_LEVEL1_CD;
        }
        else if (GameState.CURRENT_LEVEL < 2)
        {
            CURRENT_GUN_CD = GUN_LEVEL2_CD;
        }
        else if (GameState.CURRENT_LEVEL < 3)
        {
            CURRENT_GUN_CD = GUN_LEVEL3_CD;

        }
        else if (GameState.CURRENT_LEVEL < 4)
        {
            CURRENT_GUN_CD = GUN_LEVEL4_CD;

        }
        else if (GameState.CURRENT_LEVEL < 5)
        {
            CURRENT_GUN_CD = GUN_LEVEL5_CD;

        }
        else if (GameState.CURRENT_LEVEL < 6)
        {
            CURRENT_GUN_CD = GUN_LEVEL6_CD;

        }
        else if (GameState.CURRENT_LEVEL < 7)
        {
            CURRENT_GUN_CD = GUN_LEVEL7_CD;

        }
        else if (GameState.CURRENT_LEVEL < 8)
        {
            CURRENT_GUN_CD = GUN_LEVEL8_CD;

        }
        else if (GameState.CURRENT_LEVEL < 9)
        {
            CURRENT_GUN_CD = GUN_LEVEL9_CD;

        }
        else if (GameState.CURRENT_LEVEL < 10)
        {
            CURRENT_GUN_CD = GUN_LEVEL10_CD;

        }
        else if (GameState.CURRENT_LEVEL < 11)
        {
            CURRENT_GUN_CD = GUN_LEVEL11_CD;
        }
        else
        {
            CURRENT_GUN_CD = GUN_LEVEL_MAX_CD;

        }
        //switch (GameState.CURRENT_LEVEL)
        //{
        //    case 0:
        //        CURRENT_GUN_CD = GUN_LEVEL1_CD;
        //        break;
        //    case 1:
        //        CURRENT_GUN_CD = GUN_LEVEL2_CD;
        //        break;
        //    case 2:
        //        CURRENT_GUN_CD = GUN_LEVEL3_CD;
        //        break;
        //    case 3:
        //        CURRENT_GUN_CD = GUN_LEVEL4_CD;
        //        break;
        //    case 4:
        //        CURRENT_GUN_CD = GUN_LEVEL5_CD;
        //        break;
        //    case 5:
        //        CURRENT_GUN_CD = GUN_LEVEL6_CD;
        //        break;
        //        break;
        //    case 8:
        //        CURRENT_GUN_CD = GUN_LEVEL1_CD;
        //        break;
        //    default:
        //        CURRENT_GUN_CD = GUN_LEVEL1_CD;
        //        break;
        //}

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

                // TODO change fire speed with weapon level
                float move_speed = 5f;
                //rb.linearVelocity = Vector2.left * move_speed;
                rb.linearVelocity = attackDirection * move_speed;
            }

        }

    }
}