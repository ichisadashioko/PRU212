using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class UnityGameUtility
{
    public static Vector3 random_position_on_screen()
    {
        float random_position_x = UnityEngine.Random.Range(0, Screen.width);
        float random_position_y = UnityEngine.Random.Range(0, Screen.height);

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
        SWORD_PROP tmp_obj = null;
        PREDEFINED_BY_LEVEL = new();
        // 1
        PREDEFINED_BY_LEVEL.Add(new SWORD_PROP()
        {
            CD = 1f,
            Count = 1,
            Damage = 2,
            RotationSpeed = 30
        });
        // 2
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 3
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 4
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 5
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count + 1,
            Damage = tmp_obj.Damage,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 6
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage + 1,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 7
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage + 1,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 8
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count + 1,
            Damage = tmp_obj.Damage + 1,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 9
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count,
            Damage = tmp_obj.Damage + 1,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        // 10
        tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
        tmp_obj = new SWORD_PROP()
        {
            CD = tmp_obj.CD - 0.1f,
            Count = tmp_obj.Count + 1,
            Damage = tmp_obj.Damage + 1,
            RotationSpeed = tmp_obj.RotationSpeed + 10f
        };
        PREDEFINED_BY_LEVEL.Add(tmp_obj);

        for (int i = 0; i < 40; i++)
        {
            tmp_obj = PREDEFINED_BY_LEVEL[PREDEFINED_BY_LEVEL.Count - 1];
            tmp_obj = new SWORD_PROP()
            {
                CD = tmp_obj.CD - 0.1f,
                Count = tmp_obj.Count + 1,
                Damage = tmp_obj.Damage + 1,
                RotationSpeed = tmp_obj.RotationSpeed + 10f
            };
            PREDEFINED_BY_LEVEL.Add(tmp_obj);
        }
    }
    public static SWORD_PROP GetSwordPropByLevel(int level)
    {
        if (PREDEFINED_BY_LEVEL == null)
        {
            INIT_PREDEFINED_BY_LEVEL();
        }

        if (PREDEFINED_BY_LEVEL.Count == 0)
        {
            return new SWORD_PROP()
            {
                CD = 0.5f,
                Count = 4,
                Damage = 2,
                RotationSpeed = 300
            };
        }

        level = Math.Clamp(level, 0, PREDEFINED_BY_LEVEL.Count - 1);
        return PREDEFINED_BY_LEVEL[level];
    }
}
