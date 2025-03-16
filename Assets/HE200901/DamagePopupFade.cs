using UnityEngine;

public class DamagePopupFade : MonoBehaviour
{
    public float y_speed = 1f;

    public float update_delta_time = 0f;
    public float update_interval = 1f / 30f;
    public float lingering_time = 2f;
    public float created_time = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        transform.position += new Vector3(0, y_speed, 0) * Time.deltaTime;

        update_delta_time += Time.deltaTime;
        if (update_delta_time < update_interval) { return; }
        update_delta_time = 0f;
        float current_time = Time.time;
        if((current_time - created_time) < lingering_time) { return; }
        ObjectPoolManager.ReturnGameObjectToPool(gameObject);
    }
}
