using UnityEngine;

public class RedSlimeIdleAnimationSwapSprites : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public float change_frame_interval = 0.5f;
    public float last_change_frame_delta_time = 0f;
    public Sprite frame_0;
    public Sprite frame_1;
    public Sprite last_sprite = null;
    //private

    // Update is called once per frame
    void Update()
    {
        last_change_frame_delta_time += Time.deltaTime;
        if (last_change_frame_delta_time > change_frame_interval)
        {
            last_change_frame_delta_time = 0;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (frame_0 == null) { frame_0 = Resources.Load<Sprite>("red_slime_idle_frame_0"); }
                if (frame_1 == null) { frame_1 = Resources.Load<Sprite>("red_slime_idle_frame_1"); }

                if ((frame_0 != null) && (frame_1 != null))
                {
                    if (last_sprite == frame_0)
                    {
                        last_sprite = frame_1;
                        sr.sprite = frame_1;
                    }
                    else
                    {
                        last_sprite = frame_0;
                        sr.sprite = frame_0;
                    }
                }
            }
        }
    }
}
