using UnityEngine;
using System.Collections;

public class HitStop : MonoBehaviour
{

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

    // 繋いだエネルギーの情報が入ってる
    [SerializeField]
    EnergyConnect _energyConnect = null;
    public EnergyConnect energyConnect
    {
        get {
            if(_energyConnect == null)
            {
                _energyConnect = GetComponent<EnergyConnect>();
            }
            return _energyConnect;
        }
    }

    // 敵(エネルギー)のタグ
    [SerializeField]
    string _energyTagName = "Energy";

    // 相手プレイヤーのタグ
    [SerializeField]
    string _playerTagName = "Player";

    [SerializeField]
    float time = 0.5f;
    [SerializeField]
    float speed = 0.01f;

    // ヒットストップする時間
    float _stopTime = 0.0f;

    // ヒットストップ中の時間経過速度(1.0fが通常の速度)
    float _timeScale = 1.0f;

    // いまヒットストップ中かどうか
    bool _isHitStop = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isHitStop)
        {
            if (_stopTime > 0)
            {
                _stopTime -= Time.deltaTime * (1.0f / _timeScale);
            }
            else
            {
                Time.timeScale = 1.0f;
                _isHitStop = false;
            }
        }
    }

    void HitStopSet(float stopTime, float timeScale)
    {
        _stopTime = stopTime;
        Time.timeScale = timeScale;
        _timeScale = timeScale;
        _isHitStop = true;
    }

    public void OnCollisionEnter(Collision collision)
    {
        // ぶつかった物体がプレイヤーか判定
        if (collision.gameObject.tag.GetHashCode() == _playerTagName.GetHashCode())
        {
            // プレイヤーが攻撃状態なら
            if (playerState.state == PlayerState.State.ATTACK)
            {
                // その物体が狙っている物体かどうか調べる
                if (energyConnect.NextObj() == collision.gameObject)
                {
                    HitStopSet(time, speed);
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // ぶつかった物体がプレイヤーか判定
        if (other.tag.GetHashCode() == _playerTagName.GetHashCode())
        {
            // プレイヤーが攻撃状態なら
            if (playerState.state == PlayerState.State.ATTACK)
            {
                // その物体が狙っている物体かどうか調べる
                if (energyConnect.NextObj() == other.gameObject)
                {
                    HitStopSet(time, speed);
                }
            }
        }
    }
}
