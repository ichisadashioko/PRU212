using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{

    float default_scale_x;
    void Start()
    {
        //current_hp = max_hp;
        //var sr = GetComponent<Material>();
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {

            var tmp = sr.transform.localScale;
            //sr.mainTextureOffset = new Vector2((GameState.CURRENT_HP / GameState.MAX_HP), 1);
           default_scale_x=tmp.x;
        }
    }

    public float current_hp;
    float delta_update_time = 0;

    void Update()
    {
        delta_update_time+= Time.deltaTime;
        if(delta_update_time > 0.2)
        {
            //var sr = GetComponent<Material>();
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                var tmp = sr.transform.localScale;
                current_hp = GameState.CURRENT_HP;
                tmp.x = Mathf.Clamp(((float)GameState.CURRENT_HP / (float)GameState.MAX_HP), 0f, 1f) * default_scale_x;
                //tmp.x = Mathf.Max(0f, ((float)GameState.CURRENT_HP / (float)GameState.MAX_HP)) * default_scale_x;
                //tmp.x = 0.5f * default_scale_x;
                sr.transform.localScale = tmp;
                //sr.material.mainTextureOffset = new Vector2(0.5f, 1);
            }
        }
    }
}
