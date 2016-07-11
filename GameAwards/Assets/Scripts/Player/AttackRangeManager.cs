using UnityEngine;
using System.Collections;

public class AttackRangeManager : MonoBehaviour {

    // 攻撃範囲の情報
    [SerializeField]
    AttackRange[] _attackRanges = null;

    // Use this for initialization
    void Start()
    {

        // ２人以上なら攻撃範囲の情報に繋ぐ情報とプレイヤーの名前を入れる
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if (player.getPlayerType == GamePadManager.Type.ONE)
            {
                _attackRanges[0].playerName = player.name;
                _attackRanges[0].connect = player.GetComponent<EnergyConnect>();
            }
            else
            {
                _attackRanges[1].playerName = player.name;
                _attackRanges[1].connect = player.GetComponent<EnergyConnect>();
            }
        }
    }

    void Update()
    {
        //// 繋いでる方が多いと攻撃範囲がでるようにする
        //// 1Pの方が繋いでる数が多いとき
        //if (_attackRanges[0].connect.connectNum > _attackRanges[1].connect.connectNum)
        //{
        //    // 1Pの攻撃範囲を出して2Pの攻撃範囲を消す
        //    _attackRanges[0].GetComponent<MeshRenderer>().enabled = true;
        //    _attackRanges[1].GetComponent<MeshRenderer>().enabled = false;
        //}
        //// 2Pの方が繋いでる数が多いとき
        //else if (_attackRanges[1].connect.connectNum > _attackRanges[0].connect.connectNum)
        //{
        //    // 2Pの攻撃範囲を出して1Pの攻撃範囲を消す
        //    _attackRanges[0].GetComponent<MeshRenderer>().enabled = false;
        //    _attackRanges[1].GetComponent<MeshRenderer>().enabled = true;
        //}
        //// 同じ時
        //else
        //{
        //    // どちらの攻撃範囲も消す
        //    _attackRanges[0].GetComponent<MeshRenderer>().enabled = false;
        //    _attackRanges[1].GetComponent<MeshRenderer>().enabled = false;
        //}
    }
}
