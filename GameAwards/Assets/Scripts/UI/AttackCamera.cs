using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackCamera : MonoBehaviour {

    // カメラの位置
    [SerializeField]
    Camera _camera = null;

    // 初期の座標と角度を入れる
    [SerializeField]
    Vector3 _initPos;
    [SerializeField]
    Vector3 _initRotate;

    // プレイヤーとエネルギーの座標を入れるリスト
    // 全ての座標から平均値を出してそこにカメラを向ける時に使う
    //[SerializeField]
    List<Vector3> _objList = new List<Vector3>();

    // エネルギーの座標を入れる
    [SerializeField]
    List<GameObject> _energysPos = new List<GameObject>();

    // この空のオブジェクトを向けるプレイヤーに向けて動かす
    // このオブジェクトをカメラに向けてきれいに動くようにする
    GameObject _target;

    // 1P・2Pの情報を取り出すのに使う
    Attack _player1P = null;
    Attack _player2P = null;
    HpManager _1PHP = null;
    HpManager _2PHP = null;

    // ズームの力(数値が高ければ高いほどズームしない)
    [SerializeField]
    float _zoomPower = 10.0f;

    // Use this for initialization
    void Start () {
        // プレイヤーの情報を集めて 1P・2P の情報を入れる
        var players = FindObjectsOfType<Attack>();
        foreach (var player in players)
        {
            if (player.input.getPlayerType == GamePadManager.Type.ONE)
            {
                _player1P = player;
            }
            else if (player.input.getPlayerType == GamePadManager.Type.TWO)
            {
                _player2P = player;
            }
        }

        // 空のオブジェクト生成
        _target = new GameObject("AttackTarget");
        _target.transform.position = _initPos;
        Reset();
        // カメラの向いてる方向に動かす(急にカメラの向きが変わらないようにする)
        _target.transform.Translate(0.0f, 0.0f, 50.0f);

        _1PHP = _player1P.GetComponent<HpManager>();
        _2PHP = _player2P.GetComponent<HpManager>();
    }

    // リセット
    void Reset()
    {        
        _target.transform.eulerAngles = _initRotate;
        transform.eulerAngles = _initRotate;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_1PHP.getNowHp <= 0 || _2PHP.getNowHp <= 0) { return; }

        if (_player1P.playerState.state != PlayerState.State.ATTACK &&
            _player2P.playerState.state != PlayerState.State.ATTACK)
        {
            // リセット
            Reset();

            // カメラをいったん前にだして計算に必要な座標にする
            transform.Translate(0.0f, 0.0f, 50.0f);

            // 仮想ターゲットを動かす
            var posLength = (transform.position - _target.transform.position) / 5.0f;
            _target.transform.position += posLength;

            // カメラの座標を戻す
            transform.Translate(0.0f, 0.0f, -50.0f);

            ////////////////////////////////////////////////////////////////////////


            var initLength = (_initPos - transform.position) / 5.0f;
            transform.position += initLength;
        }

        // 攻撃中はカメラが動く
        else
        {
            // 座標リストをクリア
            _objList.Clear();

            // プレイヤーの情報を集める
            var _players = FindObjectsOfType<PlayerState>();
            foreach (var player in _players)
            {
                // 座標リストにプレイヤーを入れる
                _objList.Add(player.transform.position);
            }

            // エネルギーの座標を座標リストに入れる
            foreach (var energy in _energysPos)
            {
                _objList.Add(energy.transform.position);
            }

            // 全ての座標の平均値を出す
            Vector3 pos = Vector3.zero;
            foreach (var obj in _objList)
            {
                pos += new Vector3(obj.x, obj.y, obj.z);
            }

            // 平均を出すために割る
            pos /= _objList.Count;

            /////////////////////////////////////////////////////////////

            // プレイヤー同士の距離を取ってズームイン・アウトを表現してみた
            //var playerLength = _players[0].transform.position - _players[1].transform.position;
            //transform.position = new Vector3(
            // _initPos.x + Mathf.Cos(-Mathf.PI / 2.0f) * playerLength.magnitude,
            // transform.position.y,
            // _initPos.z + Mathf.Cos(-Mathf.PI / 2.0f) * playerLength.magnitude);

            // 仮想ターゲットを動かす
            var posLength = (pos - _target.transform.position) / 5.0f;
            _target.transform.position += posLength;

            var zoomPos = _initPos - (_initPos - _target.transform.position) / _zoomPower;
            var cameraLength = (zoomPos - transform.position) / 5.0f;
            transform.position += cameraLength;
        }

        // カメラを向ける
        _camera.transform.LookAt(_target.transform);
    }
}
