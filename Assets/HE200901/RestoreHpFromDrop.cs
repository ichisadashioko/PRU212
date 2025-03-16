using TMPro;
using UnityEngine;

public class RestoreHpFromDrop : MonoBehaviour
{
    public float update_interval = 0.2f;
    private float last_update_delta_time = 0;
    public int RESTORE_HP_VALUE = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time < update_interval) { return; }

        if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
        if (UnityGameUtility.check_collision(player, gameObject))
        {
            GameState.CURRENT_HP += RESTORE_HP_VALUE;
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);

            GameObject damage_popup_prefab = Resources.Load<GameObject>("damage_popup");
            if (damage_popup_prefab != null)
            {
                var _damage_popup = ObjectPoolManager.SpawnNewGameObject(damage_popup_prefab, player.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Text, false);
                var _tmp = _damage_popup.GetComponent<TextMeshPro>();
                if (_tmp != null)
                {
                    _tmp.text = $"+{RESTORE_HP_VALUE}";
                    _tmp.faceColor = new Color(0f, 1f, 0);
                    _tmp.color = new Color(0f, 1f, 0);
                }

                _damage_popup.SetActive(true);

                var _fade = _damage_popup.GetComponent<DamagePopupFade>();
                if (_fade != null)
                {
                    _fade.created_time = Time.time;
                }
            }
        }
    }
}
