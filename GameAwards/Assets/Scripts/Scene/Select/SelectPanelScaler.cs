using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 選択アイコンの大きさを変更するスクリプト
/// </summary>
public class SelectPanelScaler : MonoBehaviour {

    [SerializeField]
    private GameObject[] _panelImage = null;

    [SerializeField]
    private CharacterSelect[] _select = null;

    private GameObject[] _prevObj = null;

    private bool[] _scaleDownFlug;
    public bool[] isScaleDownFlug
    {
        get { return _scaleDownFlug; }
        set { _scaleDownFlug = value; }
    }

    //Scaleを変えるスピード
    private Vector3 MOVE_SCALE_SPEED = new Vector3(0.02f, 0.02f, 0.0f);

    private Dictionary<CharacterSelect.SkillType, GameObject> _selectObj = null;

    void Start()
    {
        _selectObj = new Dictionary<CharacterSelect.SkillType, GameObject>();
        _selectObj.Add(CharacterSelect.SkillType.Random, _panelImage[0]);
        _selectObj.Add(CharacterSelect.SkillType.SpeedController, _panelImage[1]);
        _selectObj.Add(CharacterSelect.SkillType.SuperSpeed, _panelImage[2]);
        _selectObj.Add(CharacterSelect.SkillType.Invisible, _panelImage[3]);
        _selectObj.Add(CharacterSelect.SkillType.Parry, _panelImage[4]);
        _selectObj.Add(CharacterSelect.SkillType.Tornado, _panelImage[5]);
        _selectObj.Add(CharacterSelect.SkillType.Gravity, _panelImage[6]);
        _prevObj = new GameObject[2];
        _scaleDownFlug = new bool[2];
        _scaleDownFlug[0] = true;
        _scaleDownFlug[1] = true;
        //_prevObj[0] = _panelImage[6];
        //_prevObj[1] = _panelImage[3];

    }

    void Update()
    {
        if(!_scaleDownFlug[0] || !_scaleDownFlug[1])
        {
            StartCoroutine(ScaleUp(_selectObj[_select[0].isSkillType]));
        }
        else if(!_scaleDownFlug[0] && _selectObj[_select[0].isSkillType] != _prevObj[0])
        {
            _scaleDownFlug[0] = true;
            _prevObj[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (_selectObj[_select[0].isSkillType] != _prevObj[0])
        {
            _prevObj[0] = _selectObj[_select[0].isSkillType];
            StartCoroutine(ScaleUp(_prevObj[0]));
        }


        if (!_scaleDownFlug[0] || !_scaleDownFlug[1])
        {
            StartCoroutine(ScaleUp(_selectObj[_select[1].isSkillType]));
        }
        else if (!_scaleDownFlug[1] && _selectObj[_select[1].isSkillType] != _prevObj[1])
        {
            _scaleDownFlug[1] = true;
            _prevObj[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if (_selectObj[_select[1].isSkillType] != _prevObj[1])
        {
            _prevObj[1] = _selectObj[_select[1].isSkillType];
            StartCoroutine(ScaleUp(_prevObj[1]));
        }

    }

    private IEnumerator ScaleUp(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
        var scale = obj.transform.localScale;
        while(obj == _selectObj[_select[0].isSkillType] || obj == _selectObj[_select[1].isSkillType])
        {
            scale += MOVE_SCALE_SPEED;
            obj.transform.localScale = scale;
            if(scale.x > 1.75f)
            {
                obj.transform.localScale = new Vector3(1.75f, 1.75f, 1.0f);
            }
            yield return null;
        }
        yield return StartCoroutine(ScaleDown(obj));
    }

    private IEnumerator ScaleDown(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        var scale = obj.transform.localScale;
        while(true)
        {
            scale -= MOVE_SCALE_SPEED;
            obj.transform.localScale = scale;
            if(scale.x < 1.0f)
            {
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                yield break;
            }
            yield return null;
        }
    }
}
