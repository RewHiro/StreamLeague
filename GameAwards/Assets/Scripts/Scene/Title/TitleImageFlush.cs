using UnityEngine;
using System;
using UnityEngine.UI;

public class TitleImageFlush : MonoBehaviour {

    [SerializeField]
    private int _typeNumber = 0;

    private TitleSpriteChange _spriteChange = null;

    private Vector3 _scale = Vector3.one;

    private Vector3 MOVESCALE = new Vector3(0.03f, 0.03f, 0.0f);

    void Start()
    {
        _spriteChange = GetComponent<TitleSpriteChange>();
    }

    void Update()
    {
        if(_spriteChange.spriteNumber != _typeNumber)
        {
            transform.localScale = Vector3.one;
            _scale = Vector3.one;
            return;
        }
        ColorChange();
    }

    void ColorChange()
    {
        _scale += MOVESCALE;
        if (_scale.x > 1.25f)
        {
            _scale = new Vector3(1.25f, 1.25f, 1.0f);
        }
        transform.localScale = _scale;

    }

}
