using UnityEngine;
public class RotateAroundPlayerSkill : MonoBehaviour
{
    public GameObject player;

    public float orbitRadius = 2f;
    public float orbitSpeed = 50f;
    public float created_time = 0;
    public bool modify_game_state = false;
    public float TODO_duration = 0f;

    public float angle = 0f;
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        orbitSpeed = SWORD_PROP.GetSwordPropByLevel(GameState.CURRENT_LEVEL).RotationSpeed;

        if (player != null)
        {
            angle += orbitSpeed * Time.deltaTime;

            float radians = angle * Mathf.Deg2Rad;

            float newX = player.transform.position.x + Mathf.Cos(radians) * orbitRadius;
            float newY = player.transform.position.y + Mathf.Sin(radians) * orbitRadius;

            transform.position = new Vector3(newX, newY, transform.position.z);

            transform.Rotate(0, 0, orbitSpeed * Time.deltaTime);
        }

        float exist_time = 360f / orbitSpeed;
        if ((Time.time - created_time) > exist_time)
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            if (modify_game_state)
            {
                GameState.IS_SWORD_ACTIVE = false;
            }
        }
    }
}
