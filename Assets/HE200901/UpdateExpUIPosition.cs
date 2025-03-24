using UnityEngine;

public class UpdateExpUIPosition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    RectTransform ui_element;

    // Update is called once per frame
    void Update()
    {
        if(ui_element == null){
            ui_element = GetComponent<RectTransform>();
        }
        if(ui_element == null){return;}
        //ui_element.anch
        // ui_element.anchorMin = new Vector2(0, 1);
        // ui_element.anchorMax = new Vector2(0, 1);
        // ui_element.pivot  = new Vector2(0, 0);
    }
}
