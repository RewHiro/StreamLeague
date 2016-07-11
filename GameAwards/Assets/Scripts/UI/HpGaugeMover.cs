using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/// <summary>
/// HPゲージを動かすクラス
/// </summary>
public class HpGaugeMover : MonoBehaviour {

    [SerializeField]
    private GamePadManager.Type _playerNumber = GamePadManager.Type.ONE;

    private const float STOP_TIME = 1.0f;
    private const float HALF_POINT = 1.8f;
    private const float PINCH_POINT = 4.0f;

    private const float END_TIME = 4.0f;

    private HpManager _hpManager = null;
    private float _prevHp = 0.0f;
    private Image _gaugeBar = null;

    private GameObject _cutIn = null;

    private bool _cutInEffect = true;

    void Start()
    {
        foreach(var player in FindObjectsOfType<InputBase>())
        {
            if (player.getPlayerType != _playerNumber) continue;
            _hpManager = player.GetComponent<HpManager>();
        }
        _prevHp = _hpManager.getNowHp;
        _gaugeBar = GetComponent<Image>();
        _gaugeBar.fillAmount = 0.0f;
        _cutIn = FindObjectOfType<CutInManager>().gameObject;
    }

    /// <summary>
    /// HPが減ってるかチェック
    /// </summary>
    void Update()
    {
        var nowHp = _hpManager.getNowHp;
        if(nowHp != _prevHp)
        {
            var hp = nowHp / _hpManager.getMaxHp;
            StartCoroutine(MoveGauge(_gaugeBar.fillAmount, hp, STOP_TIME));
        }
        _prevHp = nowHp;

        //ある一定の値までいったら色が変わる。
        if(nowHp < _hpManager.getMaxHp / PINCH_POINT)
        {
            _gaugeBar.color = Color.red;
        }
        else if(nowHp < _hpManager.getMaxHp / HALF_POINT)
        {
            _gaugeBar.color = Color.yellow;
        }

        if (!_cutInEffect) { return; }
        if (_cutIn == null)
        {
            _cutInEffect = false;
            StartCoroutine(StartGaugeCharge());
        }
    }

    IEnumerator StartGaugeCharge()
    {
        var time = 0.0f;
        while(time < END_TIME)
        {
            time += Time.deltaTime;
            //_gaugeBar.fillAmount = (float)Easing.InCirc(time, END_TIME, 1, 0);
            _gaugeBar.fillAmount = (float)Easing.OutCirc(time, END_TIME, 1, 0);
            yield return null;
        }
        _gaugeBar.fillAmount = 1.0f;
        yield return null;
    }

    /// <summary>
    /// ゲージを減らす処理（0～1）
    /// </summary>
    /// <param name="startHp">最初のHP</param>
    /// <param name="endHp">最後のHP</param>
    /// <returns></returns>
    IEnumerator MoveGauge(float startHp, float endHp, float endTime)
    {
        var time = 0.0f;
        while(time < endTime)
        {
            time += Time.deltaTime;

            var localGauge = _gaugeBar.fillAmount;
            localGauge = Mathf.Lerp(startHp, endHp, time / endTime);
            _gaugeBar.fillAmount = localGauge;
            
            yield return null;
        }
        yield return null;      
    }
}

