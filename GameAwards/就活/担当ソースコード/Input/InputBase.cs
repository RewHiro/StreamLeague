/// <author>
/// 新井大一
/// </author>

using UnityEngine;

public abstract class InputBase : MonoBehaviour
{
    /// <summary>
    /// プレイヤーの種類(何人目)
    /// </summary>
    [SerializeField]
    protected GamePadManager.Type _type = GamePadManager.Type.ONE;

    /// <summary>
    /// プレイヤーの種類(何人目)を取得
    /// </summary>
    public virtual GamePadManager.Type getPlayerType
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
        }
    }

    #region left

    /// <summary>
    /// 左スティックの向きを取得
    /// </summary>
    public virtual Vector3 getLeftStickDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックの水平方向軸の取得
    /// </summary>
    public virtual float getLeftHorizontal
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックの垂直方向軸の取得
    /// </summary>
    public virtual float getLeftVertical
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右に傾いているか
    /// </summary>
    public virtual bool isLeftStickRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左に傾いているか
    /// </summary>
    public virtual bool isLeftStickLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが奥に傾いているか
    /// </summary>
    public virtual bool isLeftStickFrontDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが手前に傾いているか
    /// </summary>
    public virtual bool isLeftStickBackDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが手奥に傾いているか
    /// </summary>
    public virtual bool isLeftStickUpperLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右奥に傾いているか
    /// </summary>
    public virtual bool isLeftStickUpperRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左手前に傾いているか
    /// </summary>
    public virtual bool isLeftStickLowerLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右手前に傾いているか
    /// </summary>
    public virtual bool isLeftStickLowerRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左に傾いたか
    /// </summary>
    public virtual bool isLeftStickLeftDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右に傾いたか
    /// </summary>
    public virtual bool isLeftStickRightDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが奥に傾いたか
    /// </summary>
    public virtual bool isLeftStickFrontDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが手前に傾いたか
    /// </summary>
    public virtual bool isLeftStickBackDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左奥に傾いたか
    /// </summary>
    public virtual bool isLeftStickUpperLeftDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右奥に傾いたか
    /// </summary>
    public virtual bool isLeftStickUpperRightDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左手前に傾いたか
    /// </summary>
    public virtual bool isLeftStickLowerLeftDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右手前に傾いたか
    /// </summary>
    public virtual bool isLeftStickLowerRightDown
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左にフリックしたか
    /// </summary>
    public virtual bool isLeftStickLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右にフリックしたか
    /// </summary>
    public virtual bool isLeftStickRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが奥にフリックしたか
    /// </summary>
    public virtual bool isLeftStickFrontFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが手前にフリックしたか
    /// </summary>
    public virtual bool isLeftStickBackFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左奥にフリックしたか
    /// </summary>
    public virtual bool isLeftStickUpperLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右奥にフリックしたか
    /// </summary>
    public virtual bool isLeftStickUpperRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左手前にフリックしたか
    /// </summary>
    public virtual bool isLeftStickLowerLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右手前にフリックしたか
    /// </summary>
    public virtual bool isLeftStickLowerRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックがフリックしたか
    /// </summary>
    public virtual bool isLeftStickFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが右回転したか
    /// </summary>
    public virtual bool isLeftStickClockwiseRotate
    {
        get; protected set;
    }

    /// <summary>
    /// 左スティックが左回転したか
    /// </summary>
    public virtual bool isLeftStickCounterClockwiseRotate
    {
        get; protected set;
    }


    #endregion

    #region right

    /// <summary>
    /// 右スティックの向きを取得
    /// </summary>
    public virtual Vector3 getRightStickDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックの水平方向軸の取得
    /// </summary>
    public virtual float getRightHorizontal
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックの垂直方向軸の取得
    /// </summary>
    public virtual float getRightVertical
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右に傾いているか
    /// </summary>
    public virtual bool isRightStickRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左に傾いているか
    /// </summary>
    public virtual bool isRightStickLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが億に傾いているか
    /// </summary>
    public virtual bool isRightStickFrontDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが手前に傾いているか
    /// </summary>
    public virtual bool isRightStickBackDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左奥に傾いているか
    /// </summary>
    public virtual bool isRightStickUpperLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右奥に傾いているか
    /// </summary>
    public virtual bool isRightStickUpperRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左手前に傾いているか
    /// </summary>
    public virtual bool isRightStickLowerLeftDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右手前に傾いているか
    /// </summary>
    public virtual bool isRightStickLowerRightDirection
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左フリックしたか
    /// </summary>
    public virtual bool isRightStickLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左フリックしたか
    /// </summary>
    public virtual bool isRightStickRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが奥にフリックしたか
    /// </summary>
    public virtual bool isRightStickFrontFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが手前にフリックしたか
    /// </summary>
    public virtual bool isRightStickBackFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左奥にフリックしたか
    /// </summary>
    public virtual bool isRightStickUpperLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右奥にフリックしたか
    /// </summary>
    public virtual bool isRightStickUpperRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左手前にフリックしたか
    /// </summary>
    public virtual bool isRightStickLowerLeftFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右手前にフリックしたか
    /// </summary>
    public virtual bool isRightStickLowerRightFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックがフリックしたか
    /// </summary>
    public virtual bool isRightStickFlick
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが右回転したか
    /// </summary>
    public virtual bool isRightStickClockwiseRotate
    {
        get; protected set;
    }

    /// <summary>
    /// 右スティックが左回転したか
    /// </summary>
    public virtual bool isRightStickCounterClockwiseRotate
    {
        get; protected set;
    }

    /// <summary>
    /// インビジブルコマンドが入力されたか
    /// </summary>
    public virtual bool isCommandInvisible
    {
        get; protected set;
    }

    #endregion

    #region button

    /// <summary>
    /// R2を押しているか
    /// </summary>
    public virtual bool isPressR2
    {
        get; protected set;
    }

    /// <summary>
    /// R2を押したか
    /// </summary>
    public virtual bool isPushR2
    {
        get; protected set;
    }

    /// <summary>
    /// R2離したか
    /// </summary>
    public virtual bool isReleaseR2
    {
        get; protected set;
    }

    /// <summary>
    /// L2を押しているか
    /// </summary>
    public virtual bool isPressL2
    {
        get; protected set;
    }

    /// <summary>
    /// L2を押したか
    /// </summary>
    public virtual bool isPushL2
    {
        get; protected set;
    }

    /// <summary>
    /// L2離したか
    /// </summary>
    public virtual bool isReleaseL2
    {
        get; protected set;
    }

    /// <summary>
    /// 攻撃ボタンを押したか
    /// </summary>
    public virtual bool isAttack
    {
        get; protected set;
    }

    /// <summary>
    /// 移動していたか
    /// </summary>
    public virtual bool isMoved
    {
        get; protected set;
    }

    /// <summary>
    /// 左ステップしたか
    /// </summary>
    public virtual bool isRightEscape
    {
        get; protected set;
    }

    /// <summary>
    /// 右ステップしたか
    /// </summary>
    public virtual bool isLeftEscape
    {
        get; protected set;
    }

    /// <summary>
    /// R3ボタンを押し込んだか
    /// </summary>
    public virtual bool isPushR3
    {
        get; protected set;
    }

    /// <summary>
    /// R3を離したか
    /// </summary>
    public virtual bool isReleaseR3
    {
        get; protected set;
    }

    /// <summary>
    /// スキルボタンを押したか
    /// </summary>
    public virtual bool isSkill
    {
        get; protected set;
    }

    /// <summary>
    /// 防御ボタンを押したか
    /// </summary>
    public virtual bool isDefence
    {
        get; protected set;
    }

    /// <summary>
    /// ボタンが押されているとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>押されているか</returns>
    public bool IsPress(GamePadManager.ButtonType button_type)
    {
        int num = (int)button_type + (int)_type * 20;
        KeyCode code = (KeyCode)num;
        return Input.GetKey(code);
    }

    /// <summary>
    /// ボタンが押されたとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>押されたか</returns>
    public bool IsPush(GamePadManager.ButtonType button_type)
    {
        int num = (int)button_type + (int)_type * 20;
        KeyCode code = (KeyCode)num;

        if (button_type == GamePadManager.ButtonType.A)
        {
            return Input.GetKeyDown(code) || Input.GetKeyDown(KeyCode.Return);
        }

        if (button_type == GamePadManager.ButtonType.B)
        {
            return Input.GetKeyDown(code) || Input.GetKeyDown(KeyCode.Backspace);
        }

        if (button_type == GamePadManager.ButtonType.START)
        {
            return Input.GetKeyDown(code) || Input.GetKeyDown(KeyCode.Return);
        }


        return Input.GetKeyDown(code);
    }

    /// <summary>
    /// ボタンが離されたとき
    /// </summary>
    /// <param name="button_type">ボタンの種類</param>
    /// <param name="player_type">何番目のプレイヤーか</param>
    /// <returns>離されたか</returns>
    public bool IsRelease(GamePadManager.ButtonType button_type)
    {
        int num = (int)button_type + (int)_type * 20;
        KeyCode code = (KeyCode)num;
        return Input.GetKeyUp(code);
    }

    #endregion
}
