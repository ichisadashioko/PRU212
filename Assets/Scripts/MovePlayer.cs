using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float last_handled_event_time_delta = 0;
    private float handle_event_interval = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public float move_speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        last_handled_event_time_delta += Time.deltaTime;
        if (last_handled_event_time_delta > handle_event_interval)
        {
            last_handled_event_time_delta = 0;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
            {
                Vector3 position = this.transform.position;
                position.x -= move_speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
            {
                Vector3 position = this.transform.position;
                position.x += move_speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
            {
                Vector3 position = this.transform.position;
                position.y += move_speed;
                this.transform.position = position;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
            {
                Vector3 position = this.transform.position;
                position.y -= move_speed;
                this.transform.position = position;
            }
        }
    }
}
