/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// ゲームパッドを管理するクラス
/// </summary>
public class GamePadManager : Singleton<GamePadManager>
{

    /// <summary>
    /// プレイヤーの種類
    /// </summary>
    public enum Type
    {
        ALL,
        ONE,
        TWO,
        THREE,
        FOUR
    }

    /// <summary>
    /// ボタンの種類
    /// </summary>
    public enum ButtonType
    {
        A = 330,
        B,
        X,
        Y,
        L1,
        R1,
        BACK,
        START,
        L3,
        R3,
    }

    /// <summary>
    /// 左スティックの水平方向軸の値を取得
    /// </summary>
    /// <param name="type">プレイヤーの種類</param>
    /// <returns>左スティックの水平方向軸の値</returns>
    public float GetLeftHorizontal(Type type)
    {
        var player_num = (int)type;
        return Input.GetAxis("LeftHorizontal" + player_num.ToString());
    }

    /// <summary>
    /// 左スティックの垂直方向軸の値を取得
    /// </summary>
    /// <param name="type">プレイヤーの種類</param>
    /// <returns>左スティックの垂直方向軸の値</returns>
    public float GetLeftVertical(Type type)
    {
        var player_num = (int)type;
        return -Input.GetAxis("LeftVertical" + player_num.ToString());
    }

    /// <summary>
    /// 右スティックの水平方向軸の値を取得
    /// </summary>
    /// <param name="type">プレイヤーの種類</param>
    /// <returns>右スティックの水平方向軸の値</returns>
    public float GetRightHorizontal(Type type)
    {
        var player_num = (int)type;
        return Input.GetAxis("RightHorizontal" + player_num.ToString());
    }

    /// <summary>
    /// 右スティックの垂直方向軸の値を取得
    /// </summary>
    /// <param name="type">プレイヤーの種類</param>
    /// <returns>右スティックの垂直方向軸の値</returns>
    public float GetRightVertical(Type type)
    {
        var player_num = (int)type;
        return -Input.GetAxis("RightVertical" + player_num.ToString());
    }

    /// <summary>
    /// トリガーの押し込み量を取得
    /// </summary>
    /// <param name="type">プレイヤーの種類</param>
    /// <returns>トリガーの押し込み量</returns>
    public float GetTriggerValue(Type type)
    {
        var player_num = (int)type;
        return -Input.GetAxis("R2" + player_num.ToString());
    }

    /// <summary>
    /// ボタンが押されているとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>押されているか</returns>
    public bool IsPress(ButtonType button_type, Type player_type)
    {
        int num = (int)button_type + (int)player_type * 20;
        KeyCode code = (KeyCode)num;
        return Input.GetKey(code);
    }

    /// <summary>
    /// ボタンが押されたとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>押されたか</returns>
    public bool IsPush(ButtonType button_type, Type player_type)
    {
        int num = (int)button_type + (int)player_type * 20;
        KeyCode code = (KeyCode)num;
        return Input.GetKeyDown(code);
    }

    /// <summary>
    /// ボタンが離されたとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>離されたか</returns>
    public bool IsRelease(ButtonType button_type, Type player_type)
    {
        int num = (int)button_type + (int)player_type * 20;
        KeyCode code = (KeyCode)num;
        return Input.GetKeyUp(code);
    }

    //---------------------------------------------------------------

    override protected void Awake()
    {
        base.Awake();
    }
}
