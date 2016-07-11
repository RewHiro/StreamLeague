using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCutIn : MonoBehaviour {

    // 動かす Image画像 の RectTransform
    [SerializeField]
    RectTransform _cutInImageRectTransform;

    // ゲームが始まるカウントダウンの情報
    [SerializeField]
    CutInManager _cutInManager = null;

    // 出てくるときと出ていくときのカットインの動く速さ
    [SerializeField]
    float _inOutSpeed = 5.0f;

    // 出ていく動作をするかしないか
    [SerializeField]
    bool _doOut = true;

    // 表示するときの速さ
    // Start がでるまでの時間を計算して速度を決める
    float _intervalSpeed = 0.0f;

    // カウントダウンで Start が表示されるまでの時間をいれる
    float _sumCountTime = 0.0f;

    // 動きの変化
    enum MoveType
    {
        NONE,       // カウントダウンの猶予時間の時
        IN,         // 出てくる時
        INTERVAL,   // 表示してる時
        OUT,        // 出ていく時
    }
    MoveType _type = MoveType.NONE;

    // それぞれのタイプで向かう座標の位置
    [SerializeField]
    float IN_POS_X = 3000;            // 初期位置
    [SerializeField]
    float INTERVAL_POS_X_IN = 300;    // IN の時に最初に向かう位置
    [SerializeField]
    float INTERVAL_POS_X_OUT = -300;  // INTERVAL の時に向かう位置
    [SerializeField]
    float OUT_POS_X = -3000;          // 出ていく時に向かう位置

    // 移動の時に生じる誤差(±1)
    const float ERROR_VALUE = 10.0f;

    // Use this for initialization
    void Start () {

        // 画像の位置を画面外へ
       _cutInImageRectTransform.localPosition += Vector3.right * IN_POS_X;
	}
	
	// Update is called once per frame
	void Update () {

        // MoveTypeで動きを変える
        switch (_type)
        {
            case MoveType.NONE:
                NoneMove();             // 猶予時間
                break;
            case MoveType.IN:
                InMove();               // 出る時間
                break;
            case MoveType.INTERVAL:
                IntervalMove();         // 表示する時間
                break;
            case MoveType.OUT:
                OutMove();              // 出ていく時間
                break;
        }
	}

    // カウントダウンが始まるまで
    void NoneMove()
    {
        // ディレイ時間が過ぎたら動かす
        if(_cutInManager.startDelay <= 0)
        {
            _type = MoveType.IN;
        }
    }

    // 出てくる時
    void InMove()
    {
        // Image画像の X座標を INTERVAL_POS_X_IN に移動させる計算
        var target = new Vector3(INTERVAL_POS_X_IN, _cutInImageRectTransform.localPosition.y, _cutInImageRectTransform.localPosition.z);
        var value = (target - _cutInImageRectTransform.localPosition) / _inOutSpeed;
        _cutInImageRectTransform.localPosition += value;

        if (INTERVAL_POS_X_IN < IN_POS_X)
        {
            // INTERVAL_POS_X_IN の座標に誤差の範囲内で入ったら
            if (_cutInImageRectTransform.localPosition.x <= INTERVAL_POS_X_IN + ERROR_VALUE)
            {
                // 表示するタイプに動きを変える
                _type = MoveType.INTERVAL;

                // カットインが消えるまでの合計時間を数える
                _sumCountTime += _cutInManager.destroyTime;

                // 合計時間から表示するときに動く速さを決める
                _intervalSpeed = (INTERVAL_POS_X_IN - INTERVAL_POS_X_OUT) / _sumCountTime;
            }
        }
        else
        {
            // INTERVAL_POS_X_IN の座標に誤差の範囲内で入ったら
            if (_cutInImageRectTransform.localPosition.x >= INTERVAL_POS_X_IN - ERROR_VALUE)
            {
                // 表示するタイプに動きを変える
                _type = MoveType.INTERVAL;

                // カットインが消えるまでの合計時間を数える
                _sumCountTime += _cutInManager.destroyTime;

                // 合計時間から表示するときに動く速さを決める
                _intervalSpeed = (INTERVAL_POS_X_IN - INTERVAL_POS_X_OUT) / _sumCountTime;
            }
        }
    }

    // 表示してる時
    void IntervalMove()
    {
        // MoveType.IN の時に求めた速さを加えて移動させる
        var value = Vector3.left * _intervalSpeed;
        _cutInImageRectTransform.localPosition += (value * Time.deltaTime);

        // Start が表示されたら(カウントダウンが 0 になったら)
        // かつ 出ていく動きを必要としてる時
        if (_cutInManager.destroyTime <= 0 && _doOut)
        {
            // 出ていくタイプに動きを変える
            _type = MoveType.OUT;
        }
    }

    // 出ていく時
    void OutMove()
    {
        // Image画像の X座標を OUT_POS_X に移動させる計算
        var target = new Vector3(OUT_POS_X, _cutInImageRectTransform.localPosition.y, _cutInImageRectTransform.localPosition.z);
        var value = (target - _cutInImageRectTransform.localPosition) / _inOutSpeed;
        _cutInImageRectTransform.localPosition += value;
    }
}
