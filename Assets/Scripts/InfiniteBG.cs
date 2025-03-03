using System.Collections.Generic;
using UnityEngine;

public class InfiniteBG : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject bg0 = null;
        GameObject bg1 = null;
        GameObject bg2 = null;

        if(background_list.Count != 3)
        {
            return;
        }

        bg0 = background_list[0];
        bg1 = background_list[1];
        bg2 = background_list[2];

        Vector3 bg0_pos = bg0.transform.position;
        bg0_pos.x = Camera.main.transform.position.x;
        bg0.transform.position = bg0_pos;

        Vector3 bg1_pos = bg1.transform.position;
        Vector3 bg2_pos = bg2.transform.position;
        spare_background_index = 2;
    }

    int spare_background_index = 0;
    public List<GameObject> background_list;

    // Update is called once per frame
    void Update()
    {
        //camera_width = Camera.main.
        //Camera.main.transform.position.x
    }
}
