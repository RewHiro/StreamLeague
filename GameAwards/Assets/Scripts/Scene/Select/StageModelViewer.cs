using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageModelViewer : MonoBehaviour {

    [SerializeField]
    private StageSelect _stageSelect = null;

    [SerializeField]
    private GameObject[] _stageModels = null;

    [SerializeField]
    private GameObject _selectDecidePanel = null;

    private Dictionary<StageSelect.StageType, int> _select = null;

    void Start()
    {
        _select = new Dictionary<StageSelect.StageType, int>();
        _select.Add(StageSelect.StageType.Stage1, 0);
        _select.Add(StageSelect.StageType.Stage2, 1);
        _select.Add(StageSelect.StageType.Stage3, 2);
    }

    void Update()
    {
        if(_stageSelect.isDecideStage)
        {
            _selectDecidePanel.SetActive(true);
        }
        else
        {
            _selectDecidePanel.SetActive(false);
        }

        var index = _select[_stageSelect.isStageType];
        StageModelChange(index);

    }

    void StageModelChange(int activeNumber)
    {
        for(int i = 0; i < _stageModels.Length; i++)
        {
            _stageModels[i].SetActive(false);
            _stageModels[activeNumber].SetActive(true);
        }
    }
}
