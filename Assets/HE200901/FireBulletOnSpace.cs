using UnityEngine;

public class FireBulletOnSpace : MonoBehaviour
{
    public string bullet_prefab_name = "Star_Wrath_Bullet";
    public GameObject bullet_prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }
    private float last_bullet_creation_delta_time = 0;

    // Update is called once per frame
    void Update()
    {

        last_bullet_creation_delta_time += Time.deltaTime;
        //if (last_bullet_creation_delta_time > (1f / 60f))
        // TODO change fire speed with weapon level
        float fire_rate = 1f / 10f;
        //if (last_bullet_creation_delta_time > (1f / 60f))
        if (last_bullet_creation_delta_time > fire_rate)
        {
            last_bullet_creation_delta_time = 0;
            if (!Input.GetKey(KeyCode.Space)) { return; }

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
