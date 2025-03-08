using UnityEngine;

public class FlipSpriteInMovingDirection : MonoBehaviour
{
    private float previous_x = 0;
    public bool DefaultFacingDirectionIsRight = true;

    void Update()
    {
        float current_x = gameObject.transform.position.x;
        if(current_x > previous_x)
        {
            // moving right
            FaceRight();
        }else if(current_x < previous_x)        {
            // moving left
            FaceLeft();
        }
        previous_x = current_x;
    }

    void FaceRight()
    {
        float _tmp_indicator = 1;
        if (!DefaultFacingDirectionIsRight)
        {
            _tmp_indicator = -1;
        }

        Vector3 _scale = gameObject.transform.localScale;
        var _scale_x = _scale.x;
        _scale_x = _tmp_indicator * Mathf.Abs(_scale_x);
        _scale.x = _scale_x;
        gameObject.transform.localScale = _scale;
    }
    void FaceLeft()
    {
        float _tmp_indicator = -1;
        if (!DefaultFacingDirectionIsRight)
        {
            _tmp_indicator = 1;
        }

        Vector3 _scale = gameObject.transform.localScale;
        var _scale_x = _scale.x;
        _scale_x = _tmp_indicator * Mathf.Abs(_scale_x);
        _scale.x = _scale_x;
        gameObject.transform.localScale = _scale;
    }
}