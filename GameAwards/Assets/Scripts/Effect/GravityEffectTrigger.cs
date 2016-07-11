using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// グラビティスキルのエフェクトについてるスクリプト
/// エフェクトにのっている敵プレイヤーの
/// スピードを下げる
/// </summary>
public class GravityEffectTrigger : MonoBehaviour
{
    private Gravity _gravity = null;
    private bool _trigger = false;
    private Move _playerMoveSpeed = null;
    public GameObject getUsePlayer
    {
        get; set;
    }
    private bool _start = false;

    private const int ENEMY_PLAYER_NUMBER = 0;
    private const int NORMAL_SPEED = 15;
    private const int DOWN_SPEED = 5;

    void Start()
    {
        StartCoroutine(Init());
    }

    private IEnumerator Init()
    {
        _gravity = getUsePlayer.GetComponent<Gravity>();
        _playerMoveSpeed = _gravity.playerList[ENEMY_PLAYER_NUMBER].GetComponent<Move>();
        _start = true;
        yield return null;
    }

    void Update()
    {
        if (!_start) { return; }
        if(_gravity.moveSpeedUp)
        {
            MoveSpeed(25);
        }
        else
        {
            MoveSpeed(DOWN_SPEED);
        }
    }

    //速度を変える処理
    void MoveSpeed(float speed)
    {
        if (_trigger)
        {
            //当たってたらスピードを変える
            _playerMoveSpeed.speed = speed;
        }
        else
        {
            //当たってなかったら普通のスピードにする
            _playerMoveSpeed.speed = NORMAL_SPEED;
        }
    }

    //以下当たり判定、当たったらbool切り替えてる//

    void OnTriggerEnter(Collider col)
    {
        if (!_start) { return; }
        if(col.gameObject == _gravity.playerList[ENEMY_PLAYER_NUMBER])
        {
            _trigger = true;
        }
    }

    void OnTriggerStay(Collider  col)
    {
        if (!_start) { return; }
        if (col.gameObject == _gravity.playerList[ENEMY_PLAYER_NUMBER])
        {
            _trigger = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (!_start) { return; }
        if (col.gameObject == _gravity.playerList[ENEMY_PLAYER_NUMBER])
        {
            _trigger = false;
        }
    }
}
