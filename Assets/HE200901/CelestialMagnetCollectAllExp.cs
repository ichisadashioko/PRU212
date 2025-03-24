using TMPro;
using UnityEngine;

public class CelestialMagnetCollectAllExp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public GameObject player;
    public float update_interval = 0.2f;
    private float last_update_delta_time = 0;
    // Update is called once per frame
    void Update()
    {

        last_update_delta_time += Time.deltaTime;
        if (last_update_delta_time < update_interval) { return; }

        if (player == null) { player = GameObject.FindGameObjectWithTag("Player"); }
        if (UnityGameUtility.check_collision(player, gameObject))
        {
            ObjectPoolManager.ReturnGameObjectToPool(gameObject);
            int total_exp = 0;
            var exp_obj_list = GameObject.FindGameObjectsWithTag("ExpOrb");
            foreach (GameObject exp_obj in exp_obj_list)
            {
                if (exp_obj.activeInHierarchy)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(exp_obj);
                    int exp_value = 20;
                    ExperienceManager.Instance.AddExperience(exp_value);
                    total_exp += exp_value;
                }
            }

            if (total_exp > 0)
            {

                GameObject damage_popup_prefab = Resources.Load<GameObject>("damage_popup");
                if (damage_popup_prefab != null)
                {
                    var _damage_popup = ObjectPoolManager.SpawnNewTextGameObject(damage_popup_prefab, player.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Text);
                    if(_damage_popup != null){
                    var _tmp = _damage_popup.GetComponent<TextMeshPro>();
                    if (_tmp != null)
                    {
                        _tmp.text = $"{total_exp} EXP";
                        //_tmp.color = new UnityEngine.Color(1f, 1f, 0f);
                        _tmp.color = new Color32(0, 255, 128, 255);
                        _tmp.faceColor = new Color32(0, 255, 128, 255);
                    }
                    var _fade = _damage_popup.GetComponent<DamagePopupFade>();
                    if (_fade != null)
                    {
                        _fade.created_time = Time.time;
                    }

                    }
                }
            }

            int total_restored_hp = 0;
            var hp_obj_list = GameObject.FindGameObjectsWithTag("restore_hp_item");
            foreach (GameObject hp_obj in hp_obj_list)
            {
                if (hp_obj.activeInHierarchy)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(hp_obj);
                    GameState.CURRENT_HP += 5;
                    total_restored_hp += 5;
                }
            }
            if(total_restored_hp > 0)
            {
                GameObject damage_popup_prefab = Resources.Load<GameObject>("damage_popup");
                if (damage_popup_prefab != null)
                {
                    var _damage_popup = ObjectPoolManager.SpawnNewTextGameObject(damage_popup_prefab, player.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Text);
                    if(_damage_popup != null){
                    var _tmp = _damage_popup.GetComponent<TextMeshPro>();
                    if (_tmp != null)
                    {
                        _tmp.text = $"+{total_restored_hp} HP";
                        //_tmp.color = new UnityEngine.Color(1f, 1f, 0f);
                        _tmp.color = new Color32(0, 255, 0, 255);
                        _tmp.faceColor = new Color32(0, 255, 0, 255);
                    }
                    var _fade = _damage_popup.GetComponent<DamagePopupFade>();
                    if (_fade != null)
                    {
                        _fade.created_time = Time.time;
                    }
                    }
                }
            }

            var obj_list = GameObject.FindGameObjectsWithTag("magnet_item");
            foreach (GameObject go_obj in obj_list)
            {
                if (go_obj.activeInHierarchy)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(go_obj);
                }
            }
        }
    }
}
