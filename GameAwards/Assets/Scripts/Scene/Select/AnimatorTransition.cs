using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// アイコンの切り替えのスクリプト
/// </summary>
public class AnimatorTransition : MonoBehaviour
{
    [SerializeField, Tooltip("0:選択されてないときのUI、1:選択されたときのUI")]
    private Sprite[] _sprite = null;

    [SerializeField] //スキルタイプを選択
    private CharacterSelect.SkillType _skillType = CharacterSelect.SkillType.Random;

    [SerializeField]
    private CharacterSelect[] _select = null;

    [SerializeField]
    private float _alphaCount = 0.05f;

    private float _alpha = 0.0f;

    //自分のImageを獲得
    private Image _image = null;


    void Start()
    {
        _image = GetComponent<Image>();
    }

    void LateUpdate()
    {
        if (_select[0].isSkillType == _skillType || _select[1].isSkillType == _skillType)
        {
            _image.sprite = _sprite[1];
            ColorChange();
        }
        else
        {
            _image.sprite = _sprite[0];
            _image.color = Color.white;
        }
    }

    void ColorChange()
    {
        _alpha += _alphaCount;
        if(_alpha > 1.5f)
        {
            _alpha = 1.5f;
            _alphaCount *= -1;
        }
        else if(_alpha < 0.5f)
        {
            _alpha = 0.5f;
            _alphaCount *= -1;
        }
        _image.color = new Color(1, 1, 1, _alpha);

    }

}
