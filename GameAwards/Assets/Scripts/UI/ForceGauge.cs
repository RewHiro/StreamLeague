using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForceGauge : MonoBehaviour {

    // エネルギーの画像たち
    [SerializeField]
    Image[] _energyImages;

    // UI表示に必要なプレイヤーの情報
    struct PlayerData
    {
        public EnergyConnect connect;          // 繋ぐ情報
        public CharacterParameter parameter;   // プレイヤーのパラメータ情報
    }
    PlayerData _player1P;    // 1Pプレイヤー
    PlayerData _player2P;    // 2Pプレイヤー

    string _playerTag = "Player";

	// Use this for initialization
	void Start () {
        // プレイヤーたちの情報を集める
        var players = FindObjectsOfType<InputBase>();

        // プレイヤーたちの情報から何Pかを調べて繋ぐ情報をいれる
        foreach(var player in players)
        {
            // 何Pか調べる
            if(player.getPlayerType == GamePadManager.Type.ONE)       // 1Pなら
            {
                _player1P.connect = player.GetComponent<EnergyConnect>();
                _player1P.parameter = player.GetComponent<CharacterParameter>();
            }
            else if (player.getPlayerType == GamePadManager.Type.TWO) // 2Pなら
            {
                _player2P.connect = player.GetComponent<EnergyConnect>();
                _player2P.parameter = player.GetComponent<CharacterParameter>();
            }
        }
	}

    // Update is called once per frame
    void Update()
    {
        // どちらかのHPが 0 になったら抜ける
        if (_player1P.connect.GetComponent<HpManager>().getNowHp <= 0 ||
            _player2P.connect.GetComponent<HpManager>().getNowHp <= 0)
        {
            return;
        }

        // Energy画像の色を変える
        for (int i = 0, max = _energyImages.Length; i < max; ++i)
        {
            // 1Pの判定
            if (_player1P.connect.connectList.Count > i)
            {
                // null判定
                if (_player1P.connect.connectList[_player1P.connect.connectList.Count - 1] == null)
                {
                    // 画像の色を変える
                    _energyImages[i].color = Color.white;

                    // 画像を消す
                    _energyImages[i].enabled = false;
                }
                // 最後に繋いだのがプレイヤー以外かどうか調べる
                else if (_player1P.connect.connectList[_player1P.connect.connectList.Count - 1].tag.GetHashCode() != _playerTag.GetHashCode())
                {
                    // 画像の色を変える
                    _energyImages[i].color = _player1P.parameter.getParameter.color;

                    // 画像を出す
                    _energyImages[i].enabled = true;
                }
            }
            // 2Pの判定
            else if (_energyImages.Length - _player2P.connect.connectList.Count <= i)
            {
                // null判定
                if (_player2P.connect.connectList[_player2P.connect.connectList.Count - 1] == null)
                {
                    // 画像の色を変える
                    _energyImages[i].color = Color.white;

                    // 画像を消す
                    _energyImages[i].enabled = false;
                }
                // 最後に繋いだのがプレイヤー以外かどうか調べる
                else if (_player2P.connect.connectList[_player2P.connect.connectList.Count - 1].tag.GetHashCode() != _playerTag.GetHashCode())
                {
                    // 画像の色を変える
                    _energyImages[i].color = _player2P.parameter.getParameter.color;

                    // 画像を出す
                    _energyImages[i].enabled = true;
                }
            }
            // それ以外
            else
            {
                // 画像の色を変える
                _energyImages[i].color = Color.white;

                // 画像を消す
                _energyImages[i].enabled = false;
            }
        }
    }
}
