using UnityEngine;
using System.Drawing;
public class ExpCollectible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public float update_interval = 0.1f;
    private float last_update_delta_time = 0;
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time > update_interval)
        {
            last_update_delta_time = 0;
            if(player == null){
                // TODO drop exp on death
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
                int int_scale = 1000;
                Rectangle player_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
                //Rectangle gameObject_rectangle = new Rectangle((int)(player_left_x * int_scale), (int)(player_top_y * int_scale), (int)(player_size.x * int_scale), (int)(player_size.y * int_scale));
                Rectangle gameObject_rectangle = new Rectangle((int)(gameObject_left_x * int_scale), (int)(gameObject_top_y * int_scale), (int)(gameObject_size.x * int_scale), (int)(gameObject_size.y * int_scale));
                if (!Rectangle.Intersect(player_rectangle, gameObject_rectangle).IsEmpty)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(gameObject);
                }
            }
        }
    }
}