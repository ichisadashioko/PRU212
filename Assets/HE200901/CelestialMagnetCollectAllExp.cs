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
            var exp_obj_list = GameObject.FindGameObjectsWithTag("ExpOrb");
            foreach(GameObject exp_obj in exp_obj_list)
            {
                if (exp_obj.activeInHierarchy)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(exp_obj);
                    ExperienceManager.Instance.AddExperience(20);
                }
            }
            var hp_obj_list = GameObject.FindGameObjectsWithTag("restore_hp_item");
            foreach (GameObject hp_obj in hp_obj_list)
            {
                if (hp_obj.activeInHierarchy)
                {
                    ObjectPoolManager.ReturnGameObjectToPool(hp_obj);
                    GameState.CURRENT_HP += 5;
                }
            }
        }
    }
}
