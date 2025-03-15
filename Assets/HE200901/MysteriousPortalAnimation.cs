using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MysteriousPortalAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private float change_frame_interval = 0.1f;
    public float last_change_frame_delta_time = 0f;
    public List<Sprite> sprites;
    public int current_sprite_idx = 0;
    //private

    public void load_sprites()
    {
        sprites = new();
        for (int i = 0; i < 8; i++)
        {
            string asset_path = $"Mysterious_Portal_{i}";
            var sprite = Resources.Load<Sprite>(asset_path);
            sprites.Add(sprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        last_change_frame_delta_time += Time.deltaTime;
        if (last_change_frame_delta_time > change_frame_interval)
        {
            last_change_frame_delta_time = 0;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr == null) { return; }
            if ((sprites == null) || (sprites.Count == 0))
            {
                load_sprites();
            }

            if(sprites == null) { return; }
            if(sprites.Count == 0) { return; }
            current_sprite_idx = current_sprite_idx % sprites.Count;
            var sprite = sprites[current_sprite_idx];
            if(sprite == null) { return; }
            current_sprite_idx++;
            sr.sprite = sprite;
        }
    }
}
