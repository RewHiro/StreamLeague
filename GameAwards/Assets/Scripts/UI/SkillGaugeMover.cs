using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillGaugeMover : MonoBehaviour {

    [SerializeField]
    private GamePadManager.Type _type = GamePadManager.Type.ONE;

    private SkillBase _skillBase = null;
    private float _skillGauge = 0.0f;

    [SerializeField, Tooltip("0～5:ミニゲージ, 6:ConnectLineの光, 7:とげの光, 8:パネル")]
    private GameObject[] _skillGaugeObject = null;

    [SerializeField]
    private GameObject _skillActiveUI = null;

    void Start()
    {
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if(player.getPlayerType != _type) { continue; }
            _skillBase = player.GetComponent<SkillBase>();
        }
        foreach(var obj in _skillGaugeObject)
        {
            obj.SetActive(false);
        }
        _skillGauge = _skillBase.nowCoolTime / _skillBase.EndCoolTime;
    }

    void Update()
    {
        var time = _skillBase.nowCoolTime / _skillBase.EndCoolTime;
        _skillGauge = time;

        if (_skillGauge >= 1.0f)
        {
            if (_skillGaugeObject[5].activeSelf) return;
            _skillGaugeObject[5].SetActive(true);
            _skillGaugeObject[6].SetActive(true);
            _skillGaugeObject[7].SetActive(true);
            _skillGaugeObject[8].SetActive(true);
            _skillGauge = _skillBase.EndCoolTime;
            _skillActiveUI.SetActive(true);
            AudioManager.instance.PlaySe(SoundName.SeName.skillmax);
        }
        else if (_skillGauge > 0.8f)
        {
            _skillGaugeObject[4].SetActive(true);
        }
        else if (_skillGauge > 0.6f)
        {
            _skillGaugeObject[3].SetActive(true);
        }
        else if (_skillGauge > 0.45f)
        {
            _skillGaugeObject[2].SetActive(true);
        }
        else if (_skillGauge > 0.3f)
        {
            _skillGaugeObject[1].SetActive(true);
        }
        else if (_skillGauge > 0.15f)
        {
            _skillGaugeObject[0].SetActive(true);
        }
        else
        {
            foreach(var obj in _skillGaugeObject)
            {
                obj.SetActive(false);
                _skillActiveUI.SetActive(false);
            }
        }
    }
}
