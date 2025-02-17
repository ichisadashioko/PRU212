using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovedOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    Vector3 mouse_position = Input.mousePosition;
        //    Vector3 object_size = GetComponent<SpriteRenderer>().bounds.size;
        //    Vector3 object_position = gameObject.transform.position;

        //    float left_x = object_position.x - object_size.x / 2;
        //    float right_x = object_position.x + object_size.x / 2;
        //    float top_y = object_position.y - object_size.y / 2;
        //    float bottom_y = object_position.y + object_size.y / 2;

        //    if ((mouse_position.x > left_x) && (mouse_position.x < right_x) && (mouse_position.y > top_y) && (mouse_position.y < bottom_y))
        //    {
        //        ObjectPoolManager.ReturnObjectToPool(gameObject);
        //    }
        //    //Ray ray = Camera.main.ScreenPointToRay(mouse_position);
        //    //if(Physics.Raycast(ray, out RaycastHit hit))
        //    //{
        //    //    hit.collider.hideFlags
        //    //}
        //}
    }

    private void OnMouseDown()
    {
        //Debug.Log("mousedown");
        ObjectPoolManager.ReturnGameObjectToPool(gameObject);
        SpawnEnemies.current_spawned_obj_count -= 1;
    }

    //void OnMouseDown()
    //{
    //    ObjectPoolManager.ReturnObjectToPool(gameObject);
    //}
}
