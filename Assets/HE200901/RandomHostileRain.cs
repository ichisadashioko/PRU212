using System.Drawing;
using TMPro;
using UnityEngine;

public class RandomHostileRain : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject rain_partical_system = GameObject.FindGameObjectWithTag("rain_partical_system");
        if (rain_partical_system != null)
        {
            var ps = rain_partical_system.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop();
            }
        }
    }

    public float active_duration = 60f;
    public float start_time = 0;
    public bool active = false;
    public float active_chance = 5f;

    public float deal_damage_interval = 1f;
    public GameObject player;
    public float last_deal_damage_time = 0f;
    public GameObject safe_portal_prefab;
    public float last_random_number = 0f;

    public float last_spin_chance_to_spawn_time = 0;
    public float spin_to_spawn_interval = 1f;
    public int rain_damage = 1;

    public float update_delta_time = 0f;
    public float update_interval = 1f / 30f;

    // Update is called once per frame
    void Update()
    {
        update_delta_time += Time.deltaTime;
        if (update_delta_time < update_interval) { return; }
        update_delta_time = 0f;
        float current_time = Time.time;
        if (active)
        {
            GameObject safe_portal_obj = GameObject.FindGameObjectWithTag("safe_portal");
            float delta_time = current_time - start_time;
            if (delta_time > active_duration)
            {
                active = false;
                if (safe_portal_obj != null)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(safe_portal_obj);
                }

                GameObject rain_partical_system = GameObject.FindGameObjectWithTag("rain_partical_system");
                if (rain_partical_system != null)
                {
                    var ps = rain_partical_system.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Stop();
                    }
                }

                return;
            }

            if ((current_time - last_deal_damage_time) < deal_damage_interval) { return; }
            // last_deal_damage_time = current_time;

            if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
            if (player == null) { return; }
            if (safe_portal_obj == null) { return; }
            if (!UnityGameUtility.check_collision(player, safe_portal_obj))
            {
                GameState.CURRENT_HP -= rain_damage;
                last_deal_damage_time = current_time;

                GameObject damage_popup_prefab = Resources.Load<GameObject>("damage_popup");
                if (damage_popup_prefab != null)
                {
                    var _damage_popup = ObjectPoolManager.SpawnNewGameObject(damage_popup_prefab, player.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Text);
                    var _tmp = _damage_popup.GetComponent<TextMeshPro>();
                    if (_tmp != null)
                    {
                        _tmp.text = $"-{rain_damage}";
                        _tmp.color = new UnityEngine.Color(1f, 0, 0);
                        _tmp.faceColor = new UnityEngine.Color(1f, 0, 0);
                    }
                    var _fade = _damage_popup.GetComponent<DamagePopupFade>();
                    if (_fade != null)
                    {
                        _fade.created_time = Time.time;
                    }
                }
            }
        }
        else
        {
            if ((current_time - last_spin_chance_to_spawn_time) < spin_to_spawn_interval) { return; }

            last_random_number = Random.Range(0f, 100f);
            last_spin_chance_to_spawn_time = current_time;

            if (last_random_number < active_chance)
            {
                start_time = current_time;
                active = true;

                if (safe_portal_prefab == null)
                {
                    safe_portal_prefab = Resources.Load<GameObject>("safe_portal");
                }

                // TODO spawn near player
                GameObject safe_portal_obj = ObjectPoolManager.SpawnNewGameObject(safe_portal_prefab, new Vector3(0, 0, 0), Quaternion.identity);
                GameObject rain_partical_system = GameObject.FindGameObjectWithTag("rain_partical_system");
                if (rain_partical_system != null)
                {
                    var ps = rain_partical_system.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Play();
                    }
                }
            }
        }
    }
}
