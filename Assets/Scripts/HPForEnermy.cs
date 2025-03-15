using UnityEngine;

public class HPForEnermy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float HP { get; set; }
    public void reset_hp()
    {
        HP = 2 * (1f + GameState.CURRENT_DIFFICULTY * 0.05f);
    }
}
