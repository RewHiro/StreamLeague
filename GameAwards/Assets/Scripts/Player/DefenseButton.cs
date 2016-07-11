using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefenseButton : MonoBehaviour {

    // 攻撃可能になったらプレイヤーの頭にでるImage
    [SerializeField]
    Image _buttonImage = null;

    [SerializeField]
    float _imagePosY = 0.2f;

    // 小さくすればするほど早くなります(最低値 1.0f)
    [SerializeField]
    float _imageMoveSpeedY = 3.0f;

    // 繋ぐ情報
    //[SerializeField]
    EnergyConnect _connect = null;
    public EnergyConnect connect
    {
        get { return _connect; }
        set { _connect = value; }
    }

    // 自分・相手プレイヤーを識別するための名前
    public string playerName
    {
        get; set;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // 当たり判定をプレイヤーと同じ座標にする
        gameObject.transform.position = _connect.transform.position;

        // 攻撃可能ならプレイヤーの頭に攻撃ボタンを表示する処理
        // 相手より繋いでる数が多いなら
        if (_connect.playerList[0].GetComponent<PlayerState>().state == PlayerState.State.ATTACK &&
            _connect.connectNum > 0)
        {
            // 画像を表示させるので GameObject を稼働させる
            _buttonImage.gameObject.SetActive(true);

            // ひょいっと画像を上に飛びてるようにするやつ
            var offset = new Vector3(0.0f, _imagePosY, 0.0f);
            _buttonImage.rectTransform.localPosition += (offset - _buttonImage.rectTransform.localPosition) / _imageMoveSpeedY;
        }
        else
        {
            // 画像を表示させるので GameObject を止める
            _buttonImage.gameObject.SetActive(false);

            // ひょいっと画像を上に飛びてるために位置を初期値に戻す
            _buttonImage.rectTransform.localPosition = Vector3.zero;
        }
    }
}
