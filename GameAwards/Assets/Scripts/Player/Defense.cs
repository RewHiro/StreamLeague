using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Defense : MonoBehaviour
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

    // 相手プレイヤーのタグ
    [SerializeField]
    string _playerTagName = "Player";
    public string playerTagName
    {
        get { return _playerTagName; }
    }

    // 防御の時間(秒)
    [SerializeField]
    float _defenseMaxTime = 3.0f;
    float _defenseTime = 0.0f;

    // 吹っ飛ばしの減少量(エネルギー１つで)
    [SerializeField]
    float _impactCut = 2.0f;

    // 吹っ飛ばしの減少量(エネルギー１つで)
    //[SerializeField]
    float[] _damageCut = new float[9] {
            0,
            1,
            2,
            5,
            7,
            16,
            20,
            35,
            50,
    };
    public float[] damageCut
    {
        get { return _damageCut; }
    }

    // 他プレイヤーの情報
    List<Attack> _playerList = new List<Attack>();

    ///////////////////////////////////////////////////////////
    // デバック用
    [SerializeField]
    GameObject _particlePrefab = null;
    GameObject _particleObj = null;

    [SerializeField]
    GameObject _defenceSuccessPrefab = null;

    // Use this for initialization
    void Start()
    {
        // プレイヤーたちの情報をいれる
        _playerList = _energyConnect.playerList;
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの状態が防御なら
        if (playerState.state == PlayerState.State.DEFENSE)
        {
            // 防御時間を減らす
            _defenseTime -= Time.deltaTime;

            // 防御時間が過ぎたら
            if (_defenseTime <= 0)
            {
                // プレイヤーを移動状態に戻す
                playerState.state = PlayerState.State.NORMAL;

                foreach (var energy in _energyConnect.connectList)
                {
                    Destroy(energy.gameObject);
                }

                // 繋いでるエネルギー情報・ラインを消す
                _energyConnect.ConnectReset(true);

                // パーティクルを生成してたら(デバック)
                if (_particleObj != null)
                {
                    // 消す
                    Destroy(_particleObj);
                }
            }
        }
        // プレイヤーが攻撃以外で B ボタンを押して
        // 1つ以上繋いでいたら
        else if (playerState.state == PlayerState.State.NORMAL && 
                 input.isDefence && 
                 _energyConnect.connectNum > 0)
        {
            playerState.state = PlayerState.State.DEFENSE;
            _defenseTime = _defenseMaxTime;

            if (_particlePrefab != null)
            {
                _particleObj = Instantiate(_particlePrefab);
                _particleObj.transform.parent = transform;
                _particleObj.transform.localPosition = Vector3.zero;
            }
            StartCoroutine(EnergyMove());
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        // 自分がディフェンス状態じゃないならぬける
        if(playerState.state != PlayerState.State.DEFENSE) { return; }

        // ぶつかったオブジェクトがプレイヤーか確かめる
        if(collision.gameObject.tag.GetHashCode() == _playerTagName.GetHashCode())
        {
            // 相手プレイヤーの状態
            var rivalPlayer = collision.gameObject.GetComponent<PlayerState>();

            // 相手プレイヤーの繋ぐ情報
            var rivalConnect = collision.gameObject.GetComponent<EnergyConnect>();

            // 相手プレイヤーが攻撃なら
            if (rivalPlayer.state == PlayerState.State.ATTACK)
            {
                // 相手との繋いでる差を出す
                var difference = _energyConnect.connectNum - rivalConnect.connectNum;

                // 相手より多く繋いでいたら
                if (difference > 0)
                {

                    // 多く繋いだ分だけ吹っ飛びを軽減する
                    _rigidbody.velocity /= (difference * _impactCut);   
                }

                var effect = Instantiate(_defenceSuccessPrefab);
                effect.transform.SetParent(transform);
                effect.transform.position = transform.position;

                Destroy(_particleObj);
            }
        }
    }

    IEnumerator EnergyMove()
    {
        float time = 0.0f;
        const float MOVE_TIME = 1.0f;
        while (time <= MOVE_TIME)
        {
            foreach (var energy in _energyConnect.connectList)
            {
                if (energy.tag.GetHashCode() == HashTagName.Player) continue;
                var col = energy.gameObject.GetComponent<CapsuleCollider>();
                if (col.enabled)
                {
                    energy.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                }

                energy.transform.position =
                    Vector3.Slerp(energy.transform.position, transform.position, time / MOVE_TIME);
            }
            time += Time.deltaTime;
            yield return null;
        }

        foreach (var energy in _energyConnect.connectList)
        {
            Destroy(energy);
        }

        //while (playerState.state == PlayerState.State.DEFENSE)
        //{
        //    foreach (var energy in _energyConnect.connectList)
        //    {
        //        energy.transform.position = transform.position;
        //    }
        //    yield return null;
        //}
    }
}
