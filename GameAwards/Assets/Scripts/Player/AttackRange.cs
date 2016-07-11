using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackRange : MonoBehaviour
{
    // 攻撃可能になったらプレイヤーの頭にでるImage
    [SerializeField]
    Image _buttonImage = null;

    // 画像のアニメーター
    //[SerializeField]
    //Animator _animator = null;

    //[SerializeField]
    //EnergyCreater _energyCreater = null;

    // プレイヤーから見た Image の位置
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
        // 中身がなかったら親が持ってるはずなので探す
        //if (_connect == null)
        //{
        //    _connect = GetComponentInParent<EnergyConnect>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        // 当たり判定をプレイヤーと同じ座標にする
        gameObject.transform.position = _connect.transform.position;

        // 攻撃可能ならプレイヤーの頭に攻撃ボタンを表示する処理
        // 相手より繋いでる数が多いなら
        if (_connect.attack.isCanAttack && _connect.playerList[0].GetComponent<PlayerState>().state != PlayerState.State.ATTACK)
        {
            // 画像を表示させるので GameObject を稼働させる
            _buttonImage.gameObject.SetActive(true);

            // ひょいっと画像を上に飛びてるようにするやつ
            var offset = new Vector3(0.0f, _imagePosY, 0.0f);
            _buttonImage.rectTransform.localPosition += (offset - _buttonImage.rectTransform.localPosition) / _imageMoveSpeedY;

            // アニメーションを動かす
            //_animator.enabled = true;

            //// 攻撃可能なら
            //if (_connect.attack.isCanAttack)
            //{
            //    // 色を白にする(元に戻す)
            //    _buttonImage.color = Color.white;

            //    // アニメーションを動かす
            //    _animator.enabled = true;              
            //}
            //// 攻撃不可なら
            //else
            //{
            //    // 画像を見えなくするので GameObject を停止させる
            //    //_buttonImage.gameObject.SetActive(false);

            //    // 色を灰色にする
            //    _buttonImage.color = Color.gray;

            //    // アニメーションを止める
            //    _animator.enabled = false;              
            //}
        }
        else
        {
            // 画像を表示させるので GameObject を止める
            _buttonImage.gameObject.SetActive(false);

            // ひょいっと画像を上に飛びてるために位置を初期値に戻す
            _buttonImage.rectTransform.localPosition = Vector3.zero;
        }
    }

    // OnTrigger..... の判定で同じ判定を使っていたので関数にした
    bool TriggerDecision(Collider other)
    {
        // ぶつかった物体がプレイヤーかつ
        // プレイヤーが自分と違う名前かつ
        // プレイヤーの状態が移動状態 または スキル中 ならかつ
        // １つ以上つないでいるならかつ
        // 相手より繋いでる数が多かったら
        return (other.tag.GetHashCode() == _connect.playerTagName.GetHashCode() && 
            playerName != other.name &&
            (_connect.playerState.state == PlayerState.State.NORMAL || _connect.playerState.state == PlayerState.State.SPEED_SKILL) &&
             _connect.connectList.Count >= 1 
             //&& _connect.connectList.Count > other.GetComponent<EnergyConnect>().connectList.Count
             );
    }

    public void OnTriggerEnter(Collider other)
    {
        // 判定をとる
        if (TriggerDecision(other))
        {
            // 攻撃可能にする
            _connect.attack.isCanAttack = true;
        }

        // プレイヤーかどうか見て
        if (other.tag.GetHashCode() == _connect.playerTagName.GetHashCode() &&
            other.name != _connect.name)
        {
            // 攻撃範囲内にいるので True にする
            _connect.attack.isInRange = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // 判定をとる
        if (TriggerDecision(other))
        {
            // 攻撃可能にする
            _connect.attack.isCanAttack = true;

            if (_connect.attack.isCanAttack &&       // 攻撃可能かつ
                _connect.input.isAttack)         // 攻撃ボタンを押したらかつ 
            {
                // プレイヤーをリストに追加
                _connect.connectList.Add(other.gameObject);

                // ラインを作り直す
                _connect.CreateLine();

                // 攻撃開始
                _connect.AttackStart();
            }
        }
        else
        {
            // ぶつかった物体がプレイヤーかつ
            // プレイヤーが自分と違う名前なら
            if (other.tag.GetHashCode() == _connect.playerTagName.GetHashCode() &&
            playerName != other.name)
            {
                // 攻撃不可にする
                _connect.attack.isCanAttack = false;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        // 判定をとる
        if (TriggerDecision(other))
        {
            // 攻撃できないようにする
            _connect.attack.isCanAttack = false;
        }

        // プレイヤーかどうか見て
        if (other.tag.GetHashCode() == _connect.playerTagName.GetHashCode() &&
            other.name != _connect.name)
        {
            // 攻撃範囲外にいるので False にする
            _connect.attack.isInRange = false;
        }
    }
}
