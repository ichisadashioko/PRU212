using System.Drawing;
using UnityEngine;

public class UnityGameUtility {
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
}
