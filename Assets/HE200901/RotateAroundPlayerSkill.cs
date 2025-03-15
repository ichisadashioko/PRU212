using UnityEngine;
public class RotateAroundPlayerSkill : MonoBehaviour
{
    public GameObject player;

    public float orbitRadius = 2f; // Bán kính quỹ đạo
    public float orbitSpeed = 50f; // Tốc độ quay quanh Player

    public float angle = 0f;
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (player != null)
        {
            if(GameState.CURRENT_LEVEL == 1)
            {

            }

            angle += orbitSpeed * Time.deltaTime;

            float radians = angle * Mathf.Deg2Rad;

            float newX = player.transform.position.x + Mathf.Cos(radians) * orbitRadius;
            float newY = player.transform.position.y + Mathf.Sin(radians) * orbitRadius;

            transform.position = new Vector3(newX, newY, transform.position.z);

            transform.Rotate(0, 0, orbitSpeed * Time.deltaTime);
        }
    }

}
