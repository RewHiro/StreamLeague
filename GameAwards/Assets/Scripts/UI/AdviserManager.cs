using UnityEngine;
using System.Collections;

public class AdviserManager : MonoBehaviour {

    // プレイヤーのアドバイザーたち
    [SerializeField]
    Adviser[] _advisers;

    // プレイヤーの情報
    PlayerState[] _players = null;

    // プレイヤーのHPの情報
    HpManager[] _playersHP = null;

	// Use this for initialization
	void Start () {
        // プレイヤーたちを探して入れる
        _players = FindObjectsOfType<PlayerState>();

        // 1Pのアドバイザーにプレイヤーの情報を渡す
        _advisers[0].player = _players;

        // 2Pのアドバイザーには 1P とはプレイヤーの情報を逆にして渡す
        PlayerState[] playersCopy = { _players[1], _players[0] };
        _advisers[1].player = playersCopy;

        // プレイヤーのHPの情報を入れる
        _playersHP = FindObjectsOfType<HpManager>();
    }

    void Update()
    {
        // いずれかのプレイヤーのHPが０になったらこのオブジェクトを消す
        foreach(var HP in _playersHP)
        {
            if(HP.getNowHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
