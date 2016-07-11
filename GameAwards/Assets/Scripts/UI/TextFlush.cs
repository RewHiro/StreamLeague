using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFlush : MonoBehaviour {

    private Image _image = null;

    private float _alpha = 0.0f;

    [SerializeField]
    private float _alphaCount = 0.05f;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        ColorChange();
    }

    void ColorChange()
    {
        _alpha += _alphaCount;
        if (_alpha > 1.3f)
        {
            _alpha = 1.3f;
            _alphaCount *= -1;
        }
        else if (_alpha < 0.0f)
        {
            _alpha = 0.0f;
            _alphaCount *= -1;
        }
        _image.color = new Color(1, 1, 1, _alpha);

    }


}
