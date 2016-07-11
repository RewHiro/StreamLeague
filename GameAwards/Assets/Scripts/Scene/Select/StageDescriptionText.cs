using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class StageDescriptionText : MonoBehaviour {

    public enum Type
    {
        Description,
        Name
    }

    [SerializeField]
    private Type _type = Type.Description;

    [SerializeField]
    private StageSelect _stageSelect = null;

    private Text _text = null;

    private Dictionary<Type, Action> _updateText = null;

    void Start()
    {
        _text = GetComponent<Text>();
        _updateText = new Dictionary<Type, Action>();
        _updateText.Add(Type.Description, Description);
        _updateText.Add(Type.Name, Name);
    }

    private void Name()
    {
        if(_stageSelect.isStageType == StageSelect.StageType.Stage1)
        {
            _text.text = "洞窟";
        }
        else if(_stageSelect.isStageType == StageSelect.StageType.Stage2)
        {
            _text.text = "宇宙";
        }
        else if(_stageSelect.isStageType == StageSelect.StageType.Stage3)
        {
            _text.text = "屋上";
        }
    }

    private void Description()
    {
        if (_stageSelect.isStageType == StageSelect.StageType.Stage1)
        {
            _text.text = "立体的な動きが楽しめる\n高低差ステージ";
        }
        else if (_stageSelect.isStageType == StageSelect.StageType.Stage2)
        {
            _text.text = "高度な立ち回りが要求される\nエネルギー流動ステージ";
        }
        else if (_stageSelect.isStageType == StageSelect.StageType.Stage3)
        {
            _text.text = "エネルギーが全体に配置された\nシンプルなステージ";
        }

    }

    void Update()
    {
        _updateText[_type]();
    }

}
