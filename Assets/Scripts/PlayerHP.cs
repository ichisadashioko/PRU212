using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        current_hp = max_hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int max_hp = 20;
    private int current_hp = 20;

    public void on_hit(int damage)
    {
        current_hp -= damage;
        current_hp = Mathf.Max(current_hp, 0);
        if (current_hp == 0)
        {
            // TODO
            gameObject.SetActive(false);
        }
    }
}
