using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageSpriteChange : MonoBehaviour {

    private Image _image = null;

    [SerializeField]
    private Sprite[] _sprite = null;

    [SerializeField]
    private StageSelect _select = null;

    [SerializeField]
    private float _alphaCount = 0.05f;

    private float _alpha = 0.0f;

    [SerializeField]
    private StageSelect.StageType _type = StageSelect.StageType.Stage1;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if(_select.isStageType == _type)
        {
            _image.sprite = _sprite[0];
            ColorChange();
        }
        else
        {
            _image.sprite = _sprite[1];
            _image.color = Color.white;
        }
    }


    void ColorChange()
    {
        _alpha += _alphaCount;
        if (_alpha > 1.5f)
        {
            _alpha = 1.5f;
            _alphaCount *= -1;
        }
        else if (_alpha < 0.5f)
        {
            _alpha = 0.5f;
            _alphaCount *= -1;
        }
        _image.color = new Color(1, 1, 1, _alpha);

    }


}
