using UnityEngine;

public class RemoveObjectWhenOutOfSceen : MonoBehaviour {


    // Update is called once per frame
    void Update()
    {
        float buffer_zone_width = Screen.width * 0.8f;
        Vector3 current_position_in_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        float min_screen_x = -buffer_zone_width;
        float max_screen_x = Screen.width + buffer_zone_width;
        float min_screen_y = -buffer_zone_width;
        float max_screen_y = Screen.height + buffer_zone_width;
        if (current_position_in_screen.x < min_screen_x)
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            return;
        }

        if (current_position_in_screen.x > max_screen_x)
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            return;
        }
        if (current_position_in_screen.y < min_screen_y)
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            return;
        }

        if (current_position_in_screen.y > max_screen_y)
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            return;
        }
    }
}
