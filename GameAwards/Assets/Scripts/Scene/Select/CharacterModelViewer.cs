using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterModelViewer : MonoBehaviour
{

    [SerializeField] //キャラセレクトで選択中のSkillTypeを所得
    private CharacterSelect _characterSelect = null;

    [SerializeField] //表示したいモデル
    private GameObject[] _models = null;

    [SerializeField] //選択決定したときのOKパネル
    private GameObject _selectDecidePanel = null;
    
    //選択中のモデル番号とスキルタイプを一致させる
    private Dictionary<CharacterSelect.SkillType, int> _select;

    [SerializeField] //プレイヤーの入力所得
    private PlayerInput _input = null;

    //１フレーム前のオブジェクト
    [SerializeField]
    private GameObject _prevobj = null;

    [SerializeField, Range(1, 5)]
    private int _rotateSpeed = 1;

    //クエスチョンのモデル番号をマジックナンバーじゃなくした
    private const int QUESTION_NUM = 6;

    void Start()
    {
        _select = new Dictionary<CharacterSelect.SkillType, int>();
        _select.Add(CharacterSelect.SkillType.SpeedController, 0);
        _select.Add(CharacterSelect.SkillType.SuperSpeed, 1);
        _select.Add(CharacterSelect.SkillType.Tornado, 2);
        _select.Add(CharacterSelect.SkillType.Gravity, 3);
        _select.Add(CharacterSelect.SkillType.Invisible, 4);
        _select.Add(CharacterSelect.SkillType.Parry, 5);
        _select.Add(CharacterSelect.SkillType.Random, 6);
        //_prevobj = _models[QUESTION_NUM]; //最初の表示モデル
    }

    void Update()
    {
        if (_characterSelect.isCharacterDecide)
        {
            if (_selectDecidePanel.activeSelf) { return; }
            _selectDecidePanel.SetActive(true);
            StartCoroutine(WinPose());
        }
        else
        {
            _selectDecidePanel.SetActive(false);
        }

        var index = _select[_characterSelect.isSkillType];
        ModelChange(index);
        ModelRotation(_models[index]);
    }

    private IEnumerator WinPose()
    {
        var animator = _models[(int)_characterSelect.isSkillType].GetComponent<Animator>();
        animator.SetTrigger("Select");
        yield return null;
    }

    void ModelChange(int activeNumber)
    {
        if (_models[activeNumber].activeSelf) { return; }
        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].SetActive(false);
            _models[activeNumber].SetActive(true);
        }
    }

    void ModelRotation(GameObject obj)
    {
        if (obj == _models[QUESTION_NUM]) { return; }
        if (_input.isRightStickRightDirection)
        {
            obj.transform.Rotate(Vector3.up * _rotateSpeed);
        }
        else if (_input.isRightStickLeftDirection)
        {
            obj.transform.Rotate(Vector3.up * -_rotateSpeed);
        }
        if (_prevobj != obj)
        {
            _prevobj.transform.localRotation = Quaternion.Euler(0, 213, 0);
            if (_prevobj == _models[QUESTION_NUM])
            {
                _prevobj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            _prevobj = obj;
        }
    }

}
