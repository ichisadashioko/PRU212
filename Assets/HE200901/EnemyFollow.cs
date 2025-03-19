using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 1f; // Speed at which the object moves
    private Transform target; // Target to follow

    void Start()
    {
    }

    public void move_toward_player(){
        if(target == null){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        if(target == null){return;}

        Vector2 new_pos_vector2 = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y), speed * Time.deltaTime);
        Vector3 current_pos = gameObject.transform.position;
        current_pos.x = new_pos_vector2.x;
        current_pos.y = new_pos_vector2.y;
        transform.position = current_pos;
    }

    void Update()
    {
        move_toward_player();
    }
}
