using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public float move_speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            Vector3 position = this.transform.position;
            position.x -= move_speed * Time.deltaTime;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            Vector3 position = this.transform.position;
            position.x += move_speed * Time.deltaTime;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            Vector3 position = this.transform.position;
            position.y += move_speed * Time.deltaTime;
            this.transform.position = position;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            Vector3 position = this.transform.position;
            position.y -= move_speed * Time.deltaTime;
            this.transform.position = position;
        }
    }
}
