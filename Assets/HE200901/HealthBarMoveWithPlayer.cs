using System.Drawing;
using UnityEngine;

public class HealthBarMoveWithPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public GameObject player = null;
    public GameObject health_bar_fill;
    public GameObject health_bar_background;
    private float last_update_delta_time = 0;
    public float update_interval = 0.001f;

    public Vector3 offset = new Vector3(-0.5f, 0.7f, 0);

    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time < update_interval) { return; }

        last_update_delta_time = 0;

        if (offset == null) { return; }

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (health_bar_fill == null)
        {
            health_bar_fill = GameObject.FindGameObjectWithTag("health_bar_fill");
        }
        if (health_bar_background == null)
        {
            health_bar_background = GameObject.FindGameObjectWithTag("health_bar_background");
        }

        if ((health_bar_fill == null) || (health_bar_background == null) || (player == null))
        {
            return;
        }

        Vector3 player_position = player.transform.position;
        Vector3 new_health_bar_position = player_position + offset;
        Vector3 new_health_bar_fill_position = new Vector3(new_health_bar_position.x, new_health_bar_position.y, new_health_bar_position.z - 1);
        health_bar_fill.transform.position = new_health_bar_fill_position;
        health_bar_background.transform.position = new_health_bar_position;

        //Vector3 player_size = player.GetComponent<SpriteRenderer>().bounds.size;
        //float player_left_x = player_position.x - player_size.x / 2;
        //float player_right_x = player_position.x + player_size.x / 2;
        //float player_top_y = player_position.y - player_size.y / 2;
        //float player_bottom_y = player_position.y + player_size.y / 2;


        //Vector3 gameObject_position = gameObject.transform.position;


        //Vector3 gameObject_size = gameObject.GetComponent<SpriteRenderer>().bounds.size;

        //float gameObject_left_x = gameObject_position.x - gameObject_size.x / 2;
        //float gameObject_right_x = gameObject_position.x + gameObject_size.x / 2;
        //float gameObject_top_y = gameObject_position.y - gameObject_size.y / 2;
        //float gameObject_bottom_y = gameObject_position.y + gameObject_size.y / 2;

        //Rect player_rect = new Rect(player_left_x, player_top_y, player_size.x, player_size.y);
        //int int_scale = 1000;
        //Rectangle player_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
        ////Rectangle gameObject_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
        //Rectangle gameObject_rectangle = new Rectangle((int)(gameObject_left_x * int_scale), (int)(gameObject_top_y * int_scale), (int)(gameObject_size.x * int_scale), (int)(gameObject_size.y * int_scale));
        // TODO find current player sprite size
    }
}
