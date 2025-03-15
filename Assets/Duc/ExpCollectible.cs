using UnityEngine;
using System.Drawing;
public class ExpCollectible : MonoBehaviour
{
	public float update_interval = 0.1f;
	private float last_update_delta_time = 0;
	private GameObject player;

	void Update()
	{
		last_update_delta_time += Time.deltaTime;
		if (last_update_delta_time > update_interval)
		{
			last_update_delta_time = 0;
			if (player == null)
			{
				player = GameObject.FindGameObjectWithTag("Player");
			}

			if (player != null)
			{
				if (CheckCollisionWithPlayer())
				{
					ExperienceManager.Instance.AddExperience(20);
					ObjectPoolManager.ReturnGameObjectToPool(gameObject);
				}
			}
		}
	}

	private bool CheckCollisionWithPlayer()
	{
		Vector3 player_position = player.transform.position;
		Vector3 player_size = player.GetComponent<SpriteRenderer>().bounds.size;

		Vector3 gameObject_position = transform.position;
		Vector3 gameObject_size = GetComponent<SpriteRenderer>().bounds.size;

		Rect player_rect = new Rect(player_position.x - player_size.x / 2, player_position.y - player_size.y / 2, player_size.x, player_size.y);
		Rect gameObject_rect = new Rect(gameObject_position.x - gameObject_size.x / 2, gameObject_position.y - gameObject_size.y / 2, gameObject_size.x, gameObject_size.y);

		return player_rect.Overlaps(gameObject_rect);
	}
}
