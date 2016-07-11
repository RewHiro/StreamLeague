using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// 今は２人用に作っているので３人以上にする場合は手直しします
/// </summary>
public class Adviser : MonoBehaviour {

    // プレイヤーの状況を確認するために必要なものが入ってる構造体
    struct PlayerData
    {
        public EnergyConnect _connect;
        public Attack _attack;
    }
    List<PlayerData> _datas = new List<PlayerData>();

    //[SerializeField]
    PlayerState[] _players;
    public PlayerState[] player
    {
        get { return _players; }
        set { _players = value; }
    }

    // Unity の TextComponent を入れる
    // こいつで表示する text を変える
    [SerializeField]
    Text _adviserTextMgr = null;

    // テスト用のテキスト、後に json にする
    [SerializeField]
    List<string> _texts = new List<string>()
    {
        "エネルギーを集めろ!",
        "相手にぶつかれ!",
        "エネルギーを奪え!",
        "Aボタンで攻撃しろ!",
        "あきらめろ!",
    };
    public List<string> texts
    {
        get { return _texts; }
        set { _texts = value; }
    }

    // テキストを切り替えるためのインデックス
    int _textIndex = 0;
    int _beforeTextIndex = -1;   // 前フレームと同じ Index ならテキストを更新しない

    // 生成される敵の最大数の半分をいれる
    int _energyCreatetHalfNum = 0;

    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        // プレイヤーの情報が２人分なかったら
        // 新しくプレイヤーを探して抜ける
        if(_datas.Count < 2) {
            Init();
            if (_datas.Count < 2)
            {
            }
            return;
        }

        // テキスト更新
        TextUpdate();

        /*
        "エネルギーを集めろ!",
        "相手にぶつかれ!",
        "エネルギーを奪え!",
        "Aボタンで攻撃しろ!",
        "あきらめろ!",
            */

        // 自分が攻撃可能なら
        if (_datas[0]._attack.isCanAttack)
        {
            _textIndex = 3;
        }
        // 相手が攻撃可能なら
        else if (_datas[1]._attack.isCanAttack)
        {
            _textIndex = 4;
        }
        // 相手がエネルギーを半分より多く集めていたら
        else if (_datas[1]._connect.connectNum >= _energyCreatetHalfNum)
        {
            _textIndex = 2;
        }
        // 自分がエネルギーを半分以下集めていたら
        else if (_datas[0]._connect.connectNum < _energyCreatetHalfNum)
        {
            _textIndex = 0;
        }
        // 自分がエネルギーを半分より多く集めていたら
        else if (_datas[0]._connect.connectNum >= _energyCreatetHalfNum)
        {
            _textIndex = 1;
        }
    }

    // プレイヤーの情報を集めて入れる
    // (何が起きても [0] に1Pの情報 [1]に2Pの情報が入るのでガバってます)
    void PlayersDataSet()
    {
        // すべてのプレイヤーを取り出す
        var players = GameObject.FindObjectsOfType<PlayerState>();

        // プレイヤー１人１人の情報を PlayerData に入れてリストに追加する
        foreach (var player in players)
        {
            PlayerData data;
            data._connect = player.GetComponent<EnergyConnect>();
            data._attack = player.GetComponent<Attack>();
            _datas.Add(data);
        }
    }

    // 初期化
    void Init()
    {
        // プレイヤーが２人いなかったら
        if (_players.Length != 2)
        {
            PlayersDataSet();
        }
        // プレイヤーが２人いたら
        else
        {
            // 1Pの情報を集める
            PlayerData data1P;
            data1P._connect = _players[0].GetComponent<EnergyConnect>();
            data1P._attack = _players[0].GetComponent<Attack>();
            _datas.Add(data1P);

            // 2Pの情報を集める
            PlayerData data2P;
            data2P._connect = _players[1].GetComponent<EnergyConnect>();
            data2P._attack = _players[1].GetComponent<Attack>();
            _datas.Add(data2P);
        }

        // 敵の生成情報を探していれる
        var energyCreater = FindObjectOfType<EnergyCreater>();

        // nullじゃなかったら生成されるエネルギー数の半分を取得する
        if (energyCreater != null)
        {
            _energyCreatetHalfNum = energyCreater.energyMaxNum / 2;
        }
    }

    // テキスト更新
    void TextUpdate()
    {
        // 前フレームと違う index かどうか比べて
        // テキストの要素数とインデックスを比べて要素が範囲外でないなら
        // テキストを変える
        if (_textIndex != _beforeTextIndex && _textIndex < _texts.Count)
        {
            _adviserTextMgr.text = _texts[_textIndex];
            _beforeTextIndex = _textIndex;
        }
    }
}
