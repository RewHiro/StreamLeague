using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StageIconScaler : MonoBehaviour {

    [SerializeField]
    private GameObject[] _panelImage = null;

    [SerializeField]
    private StageSelect _select = null;

    private Dictionary<StageSelect.StageType, GameObject> _selectObj = null;

    private Vector3 MOVE_SCALE_SPEED = new Vector3(0.02f, 0.02f, 0.0f);

    private GameObject _prevObj = null;

    void Start()
    {
        _selectObj = new Dictionary<StageSelect.StageType, GameObject>();
        _selectObj.Add(StageSelect.StageType.Stage1, _panelImage[0]);
        _selectObj.Add(StageSelect.StageType.Stage2, _panelImage[1]);
        _selectObj.Add(StageSelect.StageType.Stage3, _panelImage[2]);
    }

    void Update()
    {
        if(_selectObj[_select.isStageType] != _prevObj)
        {
            _prevObj = _selectObj[_select.isStageType];
            StartCoroutine(ScaleUp(_prevObj));
        }
    }

    private IEnumerator ScaleUp(GameObject obj)
    {
        var scale = obj.transform.localScale;
        while (obj == _selectObj[_select.isStageType])
        {
            scale += MOVE_SCALE_SPEED;
            obj.transform.localScale = scale;
            if (scale.x > 1.2f)
            {
                obj.transform.localScale = new Vector3(1.2f, 1.2f, 1.0f);
            }
            yield return null;
        }
        yield return StartCoroutine(ScaleDown(obj));
    }

    private IEnumerator ScaleDown(GameObject obj)
    {
        var scale = obj.transform.localScale;
        while (true)
        {
            scale -= MOVE_SCALE_SPEED;
            obj.transform.localScale = scale;
            if (scale.x < 1.0f)
            {
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                yield break;
            }
            yield return null;
        }
    }

}
