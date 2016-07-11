using UnityEngine;
using System.Collections;

public class DefenseButtonManager : MonoBehaviour {

    // 攻撃範囲の情報
    [SerializeField]
    DefenseButton[] _defenseRanges = null;

    // Use this for initialization
    void Start()
    {

        // ２人以上なら攻撃範囲の情報に繋ぐ情報とプレイヤーの名前を入れる
        foreach (var player in FindObjectsOfType<InputBase>())
        {
            if (player.getPlayerType == GamePadManager.Type.ONE)
            {
                _defenseRanges[0].playerName = player.name;
                _defenseRanges[0].connect = player.GetComponent<EnergyConnect>();
            }
            else
            {
                _defenseRanges[1].playerName = player.name;
                _defenseRanges[1].connect = player.GetComponent<EnergyConnect>();
            }
        }
    }
}
