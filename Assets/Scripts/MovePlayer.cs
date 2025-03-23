using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Animator ani = null;
    float move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public float move_speed = 2f;



    // Update is called once per frame
    void Update()
    {
        if (ani == null)
        {
            ani = GetComponent<Animator>();
        }
        float left_x = 0;
        float right_x = 0;
        float top_y = 0;
        float bottom_y = 0;

        bool has_world_border = false;
        GameObject world_bg = GameObject.FindGameObjectWithTag("world_bg");
        if (world_bg != null)
        {
            var world_bg_sr = world_bg.GetComponent<SpriteRenderer>();
            if (world_bg_sr != null)
            {
                left_x = world_bg.transform.position.x - world_bg_sr.bounds.size.x / 2f;
                right_x = world_bg.transform.position.x + world_bg_sr.bounds.size.x / 2f;

                top_y = world_bg.transform.position.y - world_bg_sr.bounds.size.y / 2f;
                bottom_y = world_bg.transform.position.y + world_bg_sr.bounds.size.y / 2f;
                has_world_border = true;
            }
        }
        move = 0;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            Vector3 position = this.transform.position;
            position.x -= move_speed * Time.deltaTime;
            this.transform.position = position;
            move = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            Vector3 position = this.transform.position;
            position.x += move_speed * Time.deltaTime;
            this.transform.position = position;
            move = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            Vector3 position = this.transform.position;
            position.y += move_speed * Time.deltaTime;
            this.transform.position = position;
            move = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            Vector3 position = this.transform.position;
            position.y -= move_speed * Time.deltaTime;
            this.transform.position = position;
            move = 1;
        }

        if (has_world_border)
        {
            Vector3 current_position = transform.position;
            current_position.x = Mathf.Clamp(current_position.x, left_x, right_x);
            current_position.y = Mathf.Clamp(current_position.y, top_y, bottom_y);
            transform.position = current_position;
        }
        else
        {
            move = 0;
        }

        if (ani != null)
        {
            ani.SetFloat("Dichuyen", move);

        }
        else
        {
            Debug.Log("no animator");
        }
    }
}
