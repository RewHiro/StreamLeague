using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// プレイヤーの攻撃する処理
/// </summary>

public class Attack : MonoBehaviour
{

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

    // 繋いだエネルギーの情報が入ってる
    [SerializeField]
    EnergyConnect _energyConnect = null;
    public EnergyConnect energyConnect
    {
        get { return _energyConnect; }
    }

    // このオブジェクトのRigidbody
    [SerializeField]
    Rigidbody _rigidbody = null;
    public Rigidbody rigidbody
    {
        get { return _rigidbody; }
    }

    // 攻撃がヒットした時のエフェクト
    [SerializeField]
    GameObject _hitParticle = null;

    [SerializeField]
    GameObject _hitPlayerEffectPrefab = null;

    // ラインを引くエフェクト
    [SerializeField]
    GameObject _lineParticle = null;

    // 敵(エネルギー)のタグ
    [SerializeField]
    string _energyTagName = "Energy";
    public string energyTagName
    {
        get { return _energyTagName; }
    }

    // 相手プレイヤーのタグ
    [SerializeField]
    string _playerTagName = "Player";
    public string playerTagName
    {
        get { return _playerTagName; }
    }

    // 攻撃の移動速度
    float _dashSpeed = 1.5f;
    public float dashSpeed
    {
        get { return _dashSpeed; }
        set { _dashSpeed = value; }
    }
    // 攻撃の移動に抑制をして慣性をつける
    const float _DASH_INERTIA = 10.0f;

    // 他プレイヤーの情報
    List<Attack> _playerList = new List<Attack>();

    // 攻撃可能かどうか
    bool _isCanAttack = false;
    public bool isCanAttack
    {
        get { return _isCanAttack; }
        set { _isCanAttack = value; }
    }

    // 攻撃範囲内にいるかどうか
    bool _isInRange = false;
    public bool isInRange
    {
        get { return _isInRange; }
        set { _isInRange = value; }
    }

    // エネルギーを通るたびに加速する
    float _speedAddPower = 1.1f;
    // いくつエネルギーを通ったか
    int _energyAddNum = 0;

    // 吹き飛ばす力
    [SerializeField]
    float _impactPower = 1.0f;

    bool _isImpact = false;
    bool _isAfterAttack = false;
    Vector3 _impactVector = Vector3.zero;
    public Vector3 impactVector
    {
        get
        {
            _isImpact = false;
            return _impactVector;
        }

        set
        {
            _isImpact = true;
            _impactVector = value;
        }
    }

    CapsuleCollider _capsulCol;

    Attack rivalAttack = null;

    // Use this for initialization
    void Start()
    {
        _playerList = _energyConnect.playerList;
        _capsulCol = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerState.state == PlayerState.State.ATTACK)
        {
            AttackUpdate();
        }
    }

    void FixedUpdate()
    {
        if (_isImpact)
        {
            //if (_isAfterAttack)
            //{
            //    rivalAttack.transform.position = transform.position;
            //    rivalAttack.transform.Translate(0, 0, 1);
            //   _isAfterAttack = false;
            //}
            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            rigidbody.isKinematic = false;
            rigidbody.velocity = impactVector;
        }
    }

    // プレイヤーの攻撃状態の情報をリセット
    // velocityZero : rigidbody.velocity を Vector3.zero にするかしないか
    // 上記はプレイヤーを吹き飛ばすのに使う
    public void ResetPlayerState(bool velocityZero = true, bool energyReset = true)
    {
        // 相手が防御なら元に戻さない
        if (playerState.state != PlayerState.State.DEFENSE)
        {
            // もし中身がなかったら攻撃をやめて移動状態に戻す
            playerState.state = PlayerState.State.NORMAL;
            _isCanAttack = false;
        }

        if (velocityZero)
        {
            _rigidbody.velocity = Vector3.zero;
        }

        _energyConnect.ConnectReset(energyReset);
    }

    // 攻撃状態時の処理
    void AttackUpdate()
    {
        // イテレータやつなぐ相手がいなかったらリセットしてメソッドを抜ける
        if (!_energyConnect.IsPresence())
        {
            //_capsulCol.isTrigger = false;
            ResetPlayerState();
            return;
        }

        //_capsulCol.isTrigger = true;

        // 向かう敵の方向を求める
        Vector3 vector = _energyConnect.NextObj().transform.position - transform.position;

        // 移動速度の倍率を増やす
        Vector3 power = vector.normalized;
        for (int i = 0; i < _energyAddNum; ++i)
        {
            power *= _speedAddPower;
        }

        // 移動
        transform.position += power * _dashSpeed * Time.deltaTime * 30;

        // 移動に慣性を持たせる
        _rigidbody.velocity += power * _dashSpeed * _DASH_INERTIA * Time.deltaTime * 30;

        transform.LookAt(vector);

        // とりあえずエフェクトをだした(デバック)
        //var obj = Instantiate(_lineParticle);
        //obj.transform.position = transform.position;
    }

    public void OnCollisionStay(Collision collision)
    {
        // ぶつかった相手がプレイヤーなら
        if (collision.gameObject.tag.GetHashCode() == _playerTagName.GetHashCode())
        {
            var attackPlayer = collision.gameObject.GetComponent<Attack>();

            if(rivalAttack == null)
            {
                rivalAttack = attackPlayer;
            }

            // プレイヤーが攻撃状態なら
            if (playerState.state == PlayerState.State.ATTACK)
            {
                // 相手が回避中なら
                if (attackPlayer.playerState.state == PlayerState.State.AVOIDANCE)
                {
                    // プレイヤーの状態を元に戻す
                    if (_energyConnect.NextObj() == collision.gameObject)
                    {
                        ResetPlayerState(false);
                        _energyAddNum = 0;
                    }
                }
                else if (attackPlayer.playerState.state == PlayerState.State.ATTACK)
                {
                    ResetPlayerState();
                    _energyAddNum = 0;
                }
                // 回避してないなら
                else
                {
                    // 相手の向きを取ってRigidbodyで弾き飛ばす
                    var vector = attackPlayer.gameObject.transform.position - transform.position;
                    //attackPlayer.rigidbody.velocity = ((vector.normalized * 50.0f + new Vector3(0.0f, 10.0f, 0.0f)) * _impactPower);
                    attackPlayer.impactVector = ((vector.normalized * 50.0f + new Vector3(0.0f, 10.0f, 0.0f)) * _impactPower);
                    attackPlayer.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    attackPlayer.rigidbody.isKinematic = true;
                    //attackPlayer.transform.Translate(vector.normalized);
                    attackPlayer.rigidbody.velocity = Vector3.zero;
                    attackPlayer.rigidbody.Sleep();

                    var vector2 = transform.position - attackPlayer.gameObject.transform.position;
                    //rigidbody.velocity = ((vector2.normalized * 50.0f + new Vector3(0.0f, 10.0f, 0.0f)) * _impactPower);
                    impactVector = ((vector2.normalized * 50.0f + new Vector3(0.0f, 10.0f, 0.0f)) * _impactPower);
                    rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                    rigidbody.isKinematic = true;
                    //transform.Translate(vector2.normalized);
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.Sleep();
                    _isAfterAttack = true;

                    var effect = Instantiate(_hitPlayerEffectPrefab);
                    effect.transform.position = collision.transform.position;

                    AudioManager.instance.PlaySe(SoundName.SeName.attack);
                    var angle = transform.rotation.eulerAngles;
                    angle.x = 0.0f;
                    angle.z = 0.0f;
                    transform.rotation = Quaternion.Euler(angle);

                    // プレイヤーの状態を元に戻す
                    if (_energyConnect.NextObj() == collision.gameObject)
                    {
                        ResetPlayerState(true);

                        if (attackPlayer._playerState.state != PlayerState.State.ATTACK)
                        {
                            attackPlayer.ResetPlayerState(true, false);
                        }
                        _energyAddNum = 0;
                    }
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        // ぶつかった物体が敵(エネルギー)か判定
        if (other.tag == _energyTagName)
        {
            // プレイヤーが攻撃状態なら
            if (playerState.state == PlayerState.State.ATTACK)
            {
                // その物体が狙っている物体かどうか調べる
                if (_energyConnect.NextObj() == other.gameObject)
                {
                    var color = GetComponent<CharacterParameter>().getParameter.color;

                    foreach (var particleEnelgy in other.GetComponentsInChildren<ParticleSystem>())
                    {
                        particleEnelgy.startColor = color;
                    }

                    ++_energyAddNum;

                    // 狙っていた物体ならエフェクトを出してその物体を消す
                    //var particle = Instantiate(_hitParticle);
                    //particle.transform.position = other.gameObject.transform.position;
                    Destroy(other.gameObject);

                    // イテレータチェック
                    _energyConnect.CheckIter();
                }
            }
        }
    }
}
