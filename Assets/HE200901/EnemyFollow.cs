using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed = 5f; // Speed at which the object moves
    private Transform target; // Target to follow

    void Start()
    {
    }

    void Update()
    {
        if(target == null){
            // Find the player GameObject with the "Player" tag
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        if (target != null)
        {
            // Move towards the player's position
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Optional: Make the object face the player
            //Vector3 direction = (target.position - transform.position).normalized;
            //if (direction != Vector3.zero)
            //{
            //    transform.forward = direction;
            //}

            // normalize z position
            Vector3 current_pos = gameObject.transform.position;
            current_pos.z = 0;
            gameObject.transform.position = current_pos;
        }
    }
}
