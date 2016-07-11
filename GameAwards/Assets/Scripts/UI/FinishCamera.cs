using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishCamera : MonoBehaviour {

    // カメラの情報
    [SerializeField]
    Camera _camera = null;

    // カメラがこの距離以下に近づいたら動かないようにするやつ
    [SerializeField]
    float _cameraLength =  10.0f;

    // 1P・2Pの情報を取り出すのに使う
    Attack _player1PAttack = null;
    Attack _player2PAttack = null;
    HpManager _player1PHP = null;
    HpManager _player2PHP = null;
    EnergyConnect _player1PConnect = null;
    EnergyConnect _player2PConnect = null;

    // ズームを切り替える
    enum Zoom
    {
        None,       // 動かさない
        Player1,    // 1Pに向ける
        Player2,    // 2Pに向ける
    }
    [SerializeField]
    Zoom _zoom = Zoom.None;

    // この空のオブジェクトを向けるプレイヤーに向けて動かす
    // このオブジェクトをカメラに向けてきれいに動くようにする
    GameObject _target;

    // Use this for initialization
    void Start () {

        // プレイヤーの情報を集めて 1P・2P の情報を入れる
        var players = FindObjectsOfType<PlayerState>();
        foreach(var player in players)
        {
            if (player.GetComponent<EnergyConnect>().input.getPlayerType == GamePadManager.Type.ONE)
            {
                _player1PAttack = player.GetComponent<Attack>();
                _player1PHP = player.GetComponent<HpManager>();
                _player1PConnect = player.GetComponent<EnergyConnect>();
            }
            else if (player.GetComponent<EnergyConnect>().input.getPlayerType == GamePadManager.Type.TWO)
            {
                _player2PAttack = player.GetComponent<Attack>();
                _player2PHP = player.GetComponent<HpManager>();
                _player2PConnect = player.GetComponent<EnergyConnect>();
            }
        }

        // 空のオブジェクト生成
        _target = new GameObject("FinishTarget");
        _target.transform.position = _camera.transform.position;
        _target.transform.eulerAngles = _camera.transform.eulerAngles;
        // カメラの向いてる方向に動かす(急にカメラの向きが変わらないようにする)
        _target.transform.Translate(0.0f, 0.0f, 50.0f);
    }
	
	// Update is called once per frame
	void Update () {
        //if (DieCheck(
        //    _player1PAttack, _player2PAttack,
        //    _player1PHP, _player2PHP,
        //    _player1PConnect, _player2PConnect))
        //{
        //    _zoom = Zoom.Player1;
        //}
        //else if (DieCheck(
        //    _player2PAttack, _player1PAttack,
        //    _player2PHP, _player1PHP,
        //    _player2PConnect, _player1PConnect))
        //{
        //    _zoom = Zoom.Player2;
        //}

        if (_player1PHP.getNowHp <= 0)
        {
            _zoom = Zoom.Player2;
        }
        else if (_player2PHP.getNowHp <= 0)
        {
            _zoom = Zoom.Player1;
        }

        if (_zoom == Zoom.Player1)
        {
            ZoomOn(_player1PHP.gameObject);
        }
        else if (_zoom == Zoom.Player2)
        {
            ZoomOn(_player2PHP.gameObject);
        }
	}

    // 死ぬ威力で一定の距離以下なら TRUE を返す
    bool DieCheck(
        Attack AT1P, Attack AT2P,
        HpManager HP1P, HpManager HP2P,
        EnergyConnect CT1P, EnergyConnect CT2P)
    {
        if(AT1P.playerState.state == PlayerState.State.ATTACK && 
            HP1P.youWillDie &&
            CT1P.NextObj() == AT2P.gameObject)
        {
            var length = AT1P.transform.position - AT2P.transform.position;
            if(length.magnitude < 2.0f)
            {
                return true;
            }
        }
        return false;
    }

    void ZoomOn(GameObject player)
    {
        {
            var targetLenght = player.transform.position - _target.transform.position;
            var moveValue = targetLenght / 10.0f;
            _target.transform.position += moveValue;
        }

        {
            var offset = new Vector3(0, 2, -5);
            var posLenght = (player.transform.position + offset)- _camera.transform.position;
            var moveValue = posLenght / 10.0f;
            _target.transform.position += moveValue;

            _camera.transform.position += moveValue;

            //if (posLenght.magnitude > _cameraLength)
            //{
            //    _camera.transform.position += moveValue;
            //}
        }

        _camera.transform.LookAt(_target.transform);
    }
}
