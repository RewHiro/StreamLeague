using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの移動の処理
/// </summary>

public class Move : MonoBehaviour {

    [SerializeField]
    InputBase _input = null;    // ゲームパッドの処理
    public InputBase input {
        get
        {
            if(_input == null)
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
    float _speed = 15.0f;    // 移動速度
    public float speed {
        get { return _speed; }
        set { _speed = value; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
            if (playerState.state == PlayerState.State.ATTACK ||
            playerState.state == PlayerState.State.AVOIDANCE) { return; }

            // 移動
            transform.Translate(input.getLeftStickDirection * _speed * Time.deltaTime, Space.World);
        if(input.getLeftStickDirection.x != 0 || input.getLeftStickDirection.z != 0)
        transform.eulerAngles = new Vector3(
            0.0f,
            -((Mathf.Atan2(input.getLeftStickDirection.z , input.getLeftStickDirection.x) - Mathf.PI / 2) * 180 / Mathf.PI),
            0.0f);
    }
}
