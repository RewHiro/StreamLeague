using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleBackSpriteChange : MonoBehaviour {

    [SerializeField]
    private SelectManager _selectManager = null;

    [SerializeField]
    private Sprite[] _sprite = null;

    private Image _image = null;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if(_selectManager.isTitleBackYes)
        {
            _image.sprite = _sprite[0];
        }
        else
        {
            _image.sprite = _sprite[1];
        }
    }

}
