using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConnectLineNumText : MonoBehaviour {

    // プレイヤーたち繋いだラインの情報
    EnergyConnect[] _players = null;

    // プレイヤーたちのテキスト
    [SerializeField]
    Text[] _lineNumTexts = null;

    // Use this for initialization
    void Start()
    {
        // プレイヤーたちの情報を集める
        _players = FindObjectsOfType<EnergyConnect>();
    }
	
	// Update is called once per frame
	void Update () {
        // プレイヤーのイテレータ
        int playerIter = 0;
	    foreach(var text in _lineNumTexts)
        {
            // プレイヤーの要素外にいったら抜ける
            // 例 : テキスト２つでプレイヤー１人しかいなかったら範囲外に行くから
            if(_players.Length == playerIter) { break; }

            // プレイヤーのつないだラインの数をテキストに更新
            text.text = _players[playerIter].connectNum.ToString();

            // プレイヤーの頭上にテキストの座標がでるようにする
            var offset = new Vector3(0.0f, _players[playerIter].transform.localScale.y, 0.0f);
            text.transform.position = _players[playerIter].transform.position + offset;

            if (_players[playerIter].GetComponent<Attack>().isCanAttack)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.black;
            }

            // 次のプレイヤーへ
            ++playerIter;
        }
	}
}
