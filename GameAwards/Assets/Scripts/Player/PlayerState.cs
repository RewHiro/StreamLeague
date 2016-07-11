using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤーの状態を管理する
/// </summary>

public class PlayerState : MonoBehaviour {

    public enum State
    {
        NORMAL,     // 通常移動
        AVOIDANCE,  // 回避中
        ATTACK,     // 攻撃中
        SPEED_SKILL,// スピードスキル中
        DEFENSE,    // 防御
    }
    public State state = State.NORMAL;
}
