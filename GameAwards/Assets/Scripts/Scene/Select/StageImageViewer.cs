using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageImageViewer : MonoBehaviour {

    [SerializeField]
    private Sprite[] _sprite = null;

    [SerializeField]
    private StageSelect _select = null;

    private Image _image = null;

    void Start()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (_select.isStageType == StageSelect.StageType.Stage1)
        {
            _image.sprite = _sprite[0];
        }
        else if (_select.isStageType == StageSelect.StageType.Stage2)
        {
            _image.sprite = _sprite[1];
        }
        else
        {
            _image.sprite = _sprite[2];
        }
    }

}
