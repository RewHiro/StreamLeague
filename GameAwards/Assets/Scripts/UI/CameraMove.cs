using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour {

    // カメラの位置
    [SerializeField]
    Camera _camera = null;

    // カメラを動かすかどうか
    [SerializeField]
    bool _cameraMoveFlag = true;
    public bool cameraMoveFlag
    {
        get { return _cameraMoveFlag; }
    }

    // カメラの初期値
    Vector3 _initCameraPos;

    // プレイヤーとエネルギーの座標を入れるリスト
    // 全ての座標から平均値を出してそこにカメラを向ける時に使う
    //[SerializeField]
    List<Vector3> _objList = new List<Vector3>();

    // エネルギーの座標を入れる
    [SerializeField]
    List<GameObject> _energysPos = new List<GameObject>();

    // カメラの角度
    float _angle = -Mathf.PI / 2.0f;

    // カメラから中心位置までの初期長さ
    float _initLength = 0.0f;

    // カメラ初期位置を覚えるためのオブジェクト
    GameObject _initPosObj = null;

    // Use this for initialization
    void Start () {

        // カメラの初期値を入れる
        // 値型じゃないから参照されてるから初期値にもどらない
        _initCameraPos = _camera.transform.position;

        // エネルギーの位置は固定なので Start で集める
        //var _energys = FindObjectsOfType<WhoHaveEnergy>();
        //foreach (var energy in _energys)
        //{
        //    _energysPos.Add(energy.transform.position);
        //}

        var length = new Vector2(transform.position.x, transform.position.z);
        _initLength = length.magnitude;

        _initPosObj = new GameObject();
        _initPosObj.transform.position = transform.position;
        _initPosObj.name = "InitCameraPos";
    }

    // Update is called once per frame
    void Update()
    {

        if (_cameraMoveFlag)
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

            // カメラを向ける
            _camera.transform.LookAt(pos);

            /////////////////////////////////////////////////////////////

            // プレイヤー同士の距離を取ってズームイン・アウトを表現してみた
            //var playerLength = _players[0].transform.position - _players[1].transform.position;
            //transform.position = new Vector3(
            //    Mathf.Cos(_angle) * _initLength + Mathf.Cos(_angle) * playerLength.magnitude,
            //    transform.position.y, 
            //    Mathf.Sin(_angle) * _initLength + Mathf.Cos(_angle) * playerLength.magnitude);

            //// カメラを回転させる
            //_angle += 0.0005f;
            
        }
        else
        {
            // カメラを初期値に戻す
            _camera.transform.position = _initCameraPos;
        }
    }
}
