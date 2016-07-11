using UnityEngine;
using System.Collections;

public class FinishSlowMotion : MonoBehaviour {

    // プレイヤーのHPの情報の配列
    //[SerializeField]
    HpManager[] _playersHP = null;

    // どのくらい遅くするか(1.0fが通常の速度)
    [SerializeField, Range(0.0f, 1.0f)]
    float _slowSpeed = 0.3f;

    // 何秒スローにするか(秒)
    [SerializeField]
    float _slowTime = 3.0f;

    // スロー中かどうか
    bool _isSlow = false;

	// Use this for initialization
	void Start () {
	    // プレイヤーの HP の情報がなかったら探す
        if (_playersHP == null)
        {
            _playersHP = FindObjectsOfType<HpManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        // スロー中なら
        if (_isSlow)
        {
            // スロー時間中なら
            if (_slowTime > 0)
            {
                // スロー時間ほ減らす
                // 計算速度が _slowSpeed 分遅れているので補正を掛けないといけない
                _slowTime -= Time.deltaTime/* * (1.0f / _slowSpeed)*/;
            }
            // スロー時間が終わったなら
            else
            {
                // スローを戻す
                Time.timeScale = 1.0f;
            }
        }
        // スロー中でないなら
        else
        {
            // HPが 0 になったプレイヤーがいないか探す
            foreach (var hp in _playersHP)
            {
                // HPが 0 以下なら
                if (hp.getNowHp <= 0)
                {
                    // スローを開始する
                    Time.timeScale = _slowSpeed;
                    _isSlow = true;
                }
            }
        }
	}
}
