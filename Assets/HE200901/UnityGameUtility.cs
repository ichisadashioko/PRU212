using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class UnityGameUtility
{
    public static Vector3 random_position_on_screen()
    {
        float random_position_x = Random.Range(0, Screen.width);
        float random_position_y = Random.Range(0, Screen.height);

        Vector3 random_position_in_game_world = Camera.main.ScreenToWorldPoint(new Vector3(random_position_x, random_position_y, 0));
        return random_position_in_game_world;
    }

    public static bool check_collision(GameObject go1, GameObject go2)
    {
        if (go1 == null) { return false; }
        if (go2 == null) { return false; }
        Vector3 go1_pos = go1.transform.position;
        var sr1 = go1.GetComponent<SpriteRenderer>();
        if (sr1 == null) { return false; }
        Vector3 go1_size = sr1.bounds.size;

        float go1_left_x = go1_pos.x - go1_size.x / 2;
        float go1_right_x = go1_pos.x + go1_size.x / 2;
        float go1_top_y = go1_pos.y - go1_size.y / 2;
        float go1_bottom_y = go1_pos.y + go1_size.y / 2;


        Vector3 go2_position = go2.transform.position;
        var sr2 = go2.GetComponent<SpriteRenderer>();
        if (sr2 == null) { return false; }
        Vector3 go2_size = sr2.bounds.size;

        float go2_left_x = go2_position.x - go2_size.x / 2;
        float go2_right_x = go2_position.x + go2_size.x / 2;
        float go2_top_y = go2_position.y - go2_size.y / 2;
        float go2_bottom_y = go2_position.y + go2_size.y / 2;

        Rect go1_rect = new Rect(go1_left_x, go1_top_y, go1_size.x, go1_size.y);
        int int_scale = 1000;
        Rectangle go1_rectangle = new Rectangle((int)(go1_left_x * int_scale), (int)(go1_top_y * int_scale), (int)(go1_size.x * int_scale), (int)(go1_size.y * int_scale));
        Rectangle go2_rectangle = new Rectangle((int)(go2_left_x * int_scale), (int)(go2_top_y * int_scale), (int)(go2_size.x * int_scale), (int)(go2_size.y * int_scale));
        if (!Rectangle.Intersect(go1_rectangle, go2_rectangle).IsEmpty)
        {
            return true;
        }

        return false;
    }

    public static float[] calculate_starting_angle_of_rotating_projectiles(int count)
    {
        List<float> angle_list = new();
        if (count <= 1)
        {
            angle_list.Add(0f);
        }
        else
        {
            float angle_step = 360f / count;
            for (int i = 0; i < count; i++)
            {
                angle_list.Add(angle_step * i);
            }
        }
        return angle_list.ToArray();
    }
}


public class SWORD_PROP
{
    public float CD { get; set; }
    public int Count { get; set; }
    public float Damage { get; set; }
    public float RotationSpeed { get; set; }

    public static List<SWORD_PROP> PREDEFINED_BY_LEVEL = null;
    public static void INIT_PREDEFINED_BY_LEVEL()
    {
        PREDEFINED_BY_LEVEL = new();
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 30
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 40
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 50
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 60
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 70
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.9f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.8f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.7f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.6f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 2,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 3,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 80
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 90
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 100
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 180
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 190
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 200
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 210
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 220
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 230
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 240
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 250
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 260
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 270
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 280
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 290
        });
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 0.5f,
            Count = 4,
            Damage = 2,
            RotationSpeed = 300
        });
    }
    public static SWORD_PROP GetSwordPropByLevel(int level)
    {
        if (PREDEFINED_BY_LEVEL == null)
        {
            INIT_PREDEFINED_BY_LEVEL();
        }

        if (level < PREDEFINED_BY_LEVEL.Count)
        {
            return PREDEFINED_BY_LEVEL[level];
        }
        //return new SWORD_PROP() { CD=Mathf.Max(0.1f, )}
        return null;
    }
}
