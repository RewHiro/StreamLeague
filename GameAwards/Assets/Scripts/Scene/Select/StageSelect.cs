using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// ステージ選択するスクリプト
/// </summary>
public class StageSelect : MonoBehaviour {

    /// <summary>
    /// ステージの種類
    /// </summary>
    public enum StageType
    {
        Stage1,
        Stage2,
        Stage3
    }

    private Dictionary<StageType, Action> _selectList = null;

    private Dictionary<StageType, Vector3> _panelPositionList = null;

    private StageType _stageType = StageType.Stage1;
    /// <summary>
    /// 現在のステージタイプを所得
    /// </summary>
    public StageType isStageType
    {
        get { return _stageType; }
    }
    private PlayerInput _playerInput = null;

    //パネル座標
    private Vector3 STAGE1_POS = new Vector3(0, 110, 0);
    private Vector3 STAGE2_POS = new Vector3(-108, -77, 0);
    private Vector3 STAGE3_POS = new Vector3(108, -77, 0);

    private bool _decideStage = false;
    /// <summary>
    /// ステージを決定したかどうか
    /// </summary>
    public bool isDecideStage
    {
        get { return _decideStage; }
        set { _decideStage = value; }
    }

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        _selectList = new Dictionary<StageType, Action>();
        _selectList.Add(StageType.Stage1, Stage1);
        _selectList.Add(StageType.Stage2, Stage2);
        _selectList.Add(StageType.Stage3, Stage3);
        _selectList[_stageType]();

        _panelPositionList = new Dictionary<StageType, Vector3>();
        _panelPositionList.Add(StageType.Stage1, STAGE1_POS);
        _panelPositionList.Add(StageType.Stage2, STAGE2_POS);
        _panelPositionList.Add(StageType.Stage3, STAGE3_POS);
        transform.localPosition = _panelPositionList[_stageType];
    }

    void Update()
    {
        if (SelectManager.isWaiting) { return; }
        if (_decideStage) { return; }
        _selectList[_stageType]();
        transform.localPosition = _panelPositionList[_stageType];
    }

    void Stage1()
    {
        if(_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _decideStage = true;
        }
        else if(_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage2;
        }
        else if(_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage3;
        }
    }

    void Stage2()
    {
        if(_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _decideStage = true;
        }
        else if(_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage1;
        }
        else if(_playerInput.isLeftStickRightDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage3;
        }
    }

    void Stage3()
    {
        if (_playerInput.IsPush(GamePadManager.ButtonType.A))
        {
            AudioManager.instance.PlaySe(SoundName.SeName.decide);
            _decideStage = true;
        }
        else if(_playerInput.isLeftStickFrontDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage1;
        }
        else if(_playerInput.isLeftStickLeftDown)
        {
            AudioManager.instance.PlaySe(SoundName.SeName.cursor);
            _stageType = StageType.Stage2;
        }
    }
}
