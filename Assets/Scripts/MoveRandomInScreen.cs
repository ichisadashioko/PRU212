using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRandomInScreen : MonoBehaviour
{
    //public GameObject game_object;
    // Start is called before the first frame update
    void Start()
    {
        destination = RandomEdgeLocation();
        move_speed = Random.Range(0.1f, 0.6f);
    }

    public Vector3 RandomEdgeLocation()
    {
        // random location at the edge of the screen
        int random_edge_location_x = 0;
        int random_edge_location_y = 0;

        float edge_selector_random = Random.Range(0, 4);

        if (edge_selector_random < 1)
        {
            random_edge_location_x = 0;
            random_edge_location_y = Random.Range(0, Screen.height);
        }
        else if (edge_selector_random < 2)
        {
            random_edge_location_x = Screen.width;
            random_edge_location_y = Random.Range(0, Screen.height);

        }
        else if (edge_selector_random < 3)
        {
            random_edge_location_x = Random.Range(0, Screen.width);
            random_edge_location_y = 0;

        }
        else
        {
            random_edge_location_x = Random.Range(0, Screen.width);
            random_edge_location_y = Screen.height;
        }

        Vector3 retval = Camera.main.ScreenToWorldPoint(new Vector3(random_edge_location_x, random_edge_location_y, 0));
        retval.z = 0;
        return retval;
    }

    public float update_interval = 0.1f;
    private float last_update_delta_time = 0;

    public Vector3 destination;
    public float move_speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time > update_interval)
        {
            last_update_delta_time = 0;
            Vector3 current_position = gameObject.transform.position;
            if (destination == null)
            {
                destination = RandomEdgeLocation();
            }
            else if (Vector3.Distance(current_position, destination) < 0.1f)
            {
                destination = RandomEdgeLocation();
            }

            Vector3 new_position = Vector3.MoveTowards(current_position, destination, move_speed);
            new_position.z = 0;
            gameObject.transform.position = new_position;
        }
    }
}
