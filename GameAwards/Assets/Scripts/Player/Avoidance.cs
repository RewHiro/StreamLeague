using UnityEngine;
using System.Collections;

public class Avoidance : MonoBehaviour {

    [SerializeField]
    InputBase _input = null;    // ゲームパッドの処理
    public InputBase input
    {
        get
        {
            if (_input == null)
            {
                _input = GetComponent<InputBase>();
            }
            return _input;
        }
    }

    [SerializeField]
    PlayerState _playerState = null;    // プレイヤーの状態
    public PlayerState playerState
    {
        get
        {
            if (_playerState == null)
            {
                _playerState = GetComponent<PlayerState>();
            }
            return _playerState;
        }
    }

    [SerializeField]
    float _speed = 0.8f;        // 回避(ステップ)の初動の速さ
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    float _speedValue = 0.0f;   // 回避の速さの大きさ(だんだん値が減って回避の終わり際に遅くなる)

    [SerializeField]
    float _durationTime = 0.05f;     // 回避の継続時間(秒)
    public float durationTime
    {
        get { return _durationTime; }
        set { _durationTime = value; }
    }
    float _durationCounter = 0.0f;  // 継続時間 : 回避の継続時間を数える

    [SerializeField]
    float _delayTime = 0.5f;    // 回避(ステップ)の使用間隔(秒)
    public float delayTime
    {
        get { return _delayTime; }
        set { _delayTime = value; }
    }
    float _delayCounter = 0.0f; // ディレイ時間 : 回避(ステップ)の使用間隔の時間を数える 
                                // ( 0.0f 以下になったら再び回避できるようになる。)

    Vector3 vector = Vector3.zero;  // 移動の向き

    [SerializeField]
    GameObject _particleDebug = null;   // 回避してるかわからないから見た目をつけてデバッグ

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        switch (playerState.state)
        {
            case PlayerState.State.NORMAL:
                // 移動時の処理
                NormalUpdate();
                break;
            case PlayerState.State.AVOIDANCE:
                // 回避処理
                AvoidanceUpdate();
                break;
        }
	}

    // 移動状態時の回避処理
    void NormalUpdate()
    {
        // 回避が使えるか使えないかの判定
        // ディレイ時間が 0.0f 以下になれば使える
        if (_delayCounter <= 0.0f)
        {
            // 左スティックをはじいたかの判定
            if (input.isLeftEscape)
            {
                // プレイヤーの状態を回避に変える
                playerState.state = PlayerState.State.AVOIDANCE;

                // プレイヤーの移動方向の向きを取る(スティックを倒した方向)
                vector = new Vector3(-1.0f, 0.0f, 0.0f); ;

                // それぞれの数えるカウンタに数値を入れる
                _delayCounter = _delayTime;
                _durationCounter = _durationTime;

                // 初速度を入れる
                _speedValue = _speed;
            }
            else if (input.isRightEscape)
            {
                // プレイヤーの状態を回避に変える
                playerState.state = PlayerState.State.AVOIDANCE;

                // プレイヤーの移動方向の向きを取る(スティックを倒した方向)
                vector = new Vector3(1.0f, 0.0f, 0.0f);

                // それぞれの数えるカウンタに数値を入れる
                _delayCounter = _delayTime;
                _durationCounter = _durationTime;

                // 初速度を入れる
                _speedValue = _speed;
            }
        }
        else
        {
            // できない時間ならディレイ時間を減らす
            _delayCounter -= Time.deltaTime;
        }
    }

    // 回避処理時の回避処理
    void AvoidanceUpdate()
    {
        //移動
        transform.Translate(vector * _speedValue);

        // 速度を落としていく
        _speedValue -= 1.0f * Time.deltaTime;

        // 回避の継続時間なら数値を減らして
        // 継続時間が終わったらプレイヤーの状態を回避から移動状態に戻す
        if (_durationCounter > 0.0f)
        {
            _durationCounter -= Time.deltaTime;
        }
        else
        {
            playerState.state = PlayerState.State.NORMAL;
        }

        // デバック用のparticle (回避してるかわからないから付けた)
        var obj = Instantiate(_particleDebug);
        obj.transform.position = transform.position;
    }
}
