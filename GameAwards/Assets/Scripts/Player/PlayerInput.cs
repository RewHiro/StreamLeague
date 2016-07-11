/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// プレイヤーの入力クラス
/// </summary>
public class PlayerInput : InputBase
{

    /// <summary>
    /// プレイヤーの種類(何人目)を取得
    /// </summary>
    public override GamePadManager.Type getPlayerType
    {
        get
        {
            return _type;
        }

        set
        {
            _type = value;
            _leftStickInput = new LeftStickInput(_gamePadManager, _type);
            _rightStickInput = new RightStickInput(_gamePadManager, _type);
        }
    }

    #region left

    /// <summary>
    /// 左スティックの向きを取得
    /// </summary>
    public override Vector3 getLeftStickDirection
    {
        get
        {
            return _leftStickInput.getDirection;
        }
    }

    /// <summary>
    /// 左スティックの水平方向軸の取得
    /// </summary>
    public override float getLeftHorizontal
    {
        get
        {
            return _leftStickInput.getHorizontal;
        }
    }

    /// <summary>
    /// 左スティックの垂直方向軸の取得
    /// </summary>
    public override float getLeftVertical
    {
        get
        {
            return _leftStickInput.getVertical;
        }
    }

    /// <summary>
    /// 左スティックが右に傾いているか
    /// </summary>
    public override bool isLeftStickRightDirection
    {
        get
        {
            return _leftStickInput.isDirectionRight;
        }
    }

    /// <summary>
    /// 左スティックが左に傾いているか
    /// </summary>
    public override bool isLeftStickLeftDirection
    {
        get
        {
            return _leftStickInput.isDirectionLeft;
        }
    }

    /// <summary>
    /// 左スティックが奥に傾いているか
    /// </summary>
    public override bool isLeftStickFrontDirection
    {
        get
        {
            return _leftStickInput.isDirectionFront;
        }
    }

    /// <summary>
    /// 左スティックが手前に傾いているか
    /// </summary>
    public override bool isLeftStickBackDirection
    {
        get
        {
            return _leftStickInput.isDirectionBack;
        }
    }

    /// <summary>
    /// 左スティックが左奥に傾いているか
    /// </summary>
    public override bool isLeftStickUpperLeftDirection
    {
        get
        {
            return _leftStickInput.isDirectionUpperLeft;
        }
    }

    /// <summary>
    /// 左スティックが右奥に傾いているか
    /// </summary>
    public override bool isLeftStickUpperRightDirection
    {
        get
        {
            return _leftStickInput.isDirectionUpperRight;
        }
    }

    /// <summary>
    /// 左スティックが左奥に傾いているか
    /// </summary>
    public override bool isLeftStickLowerLeftDirection
    {
        get
        {
            return _leftStickInput.isDirectionLowerLeft;
        }
    }

    /// <summary>
    /// 左スティックが右手前に傾いているか
    /// </summary>
    public override bool isLeftStickLowerRightDirection
    {
        get
        {
            return _leftStickInput.isDirectionLowerRight;
        }
    }

    /// <summary>
    /// 左スティックが左手前に傾いたか
    /// </summary>
    public override bool isLeftStickLeftDown
    {
        get
        {
            return _leftStickInput.isDownLeft;
        }
    }

    /// <summary>
    /// 左スティックが右に傾いたか
    /// </summary>
    public override bool isLeftStickRightDown
    {
        get
        {
            return _leftStickInput.isDownRight;
        }
    }

    /// <summary>
    /// 左スティックが奥に傾いたか
    /// </summary>
    public override bool isLeftStickFrontDown
    {
        get
        {
            return _leftStickInput.isDownFront;
        }
    }

    /// <summary>
    /// 左スティックが手前に傾いたか
    /// </summary>
    public override bool isLeftStickBackDown
    {
        get
        {
            return _leftStickInput.isDownBack;
        }
    }

    /// <summary>
    /// 左スティックが左奥に傾いたか
    /// </summary>
    public override bool isLeftStickUpperLeftDown
    {
        get
        {
            return _leftStickInput.isDownUpperLeft;
        }
    }

    /// <summary>
    /// 左スティックが右奥に傾いたか
    /// </summary
    public override bool isLeftStickUpperRightDown
    {
        get
        {
            return _leftStickInput.isDownUpperRight;
        }
    }

    /// <summary>
    /// 左スティックが左手前に傾いたか
    /// </summary>
    public override bool isLeftStickLowerLeftDown
    {
        get
        {
            return _leftStickInput.isDownLowerLeft;
        }
    }

    /// <summary>
    /// 左スティックが右手前に傾いたか
    /// </summary>
    public override bool isLeftStickLowerRightDown
    {
        get
        {
            return _leftStickInput.isDownLowerRight;
        }
    }

    /// <summary>
    /// 左スティックが左にフリックしたか
    /// </summary>
    public override bool isLeftStickLeftFlick
    {
        get
        {
            return _leftStickInput.isFlickLeft;
        }
    }

    /// <summary>
    /// 左スティックが右にフリックしたか
    /// </summary>
    public override bool isLeftStickRightFlick
    {
        get
        {
            return _leftStickInput.isFlickRight;
        }
    }

    /// <summary>
    /// 左スティックが奥にフリックしたか
    /// </summary>
    public override bool isLeftStickFrontFlick
    {
        get
        {
            return _leftStickInput.isFlickFront;
        }
    }

    /// <summary>
    /// 左スティックが手前にフリックしたか
    /// </summary>
    public override bool isLeftStickBackFlick
    {
        get
        {
            return _leftStickInput.isFlickBack;
        }
    }

    /// <summary>
    /// 左スティックが左奥にフリックしたか
    /// </summary>
    public override bool isLeftStickUpperLeftFlick
    {
        get
        {
            return _leftStickInput.isFlickUpperLeft;
        }
    }

    /// <summary>
    /// 左スティックが右奥にフリックしたか
    /// </summary>
    public override bool isLeftStickUpperRightFlick
    {
        get
        {
            return _leftStickInput.isFlickUpperRight;
        }
    }

    /// <summary>
    /// 左スティックが左手前にフリックしたか
    /// </summary>
    public override bool isLeftStickLowerLeftFlick
    {
        get
        {
            return _leftStickInput.isFlickLowerLeft;
        }
    }

    /// <summary>
    /// 左スティックが右手前にフリックしたか
    /// </summary>
    public override bool isLeftStickLowerRightFlick
    {
        get
        {
            return _leftStickInput.isFlickLowerRight;
        }
    }

    /// <summary>
    /// 左スティックがフリックしたか
    /// </summary>
    public override bool isLeftStickFlick
    {
        get
        {
            return _leftStickInput.isFlick;
        }
    }

    /// <summary>
    /// 左スティックが右回転したか
    /// </summary>
    public override bool isLeftStickClockwiseRotate
    {
        get
        {
            return _leftStickInput.isSuccessClockwiseRotate;
        }
    }

    /// <summary>
    /// 左スティックが左回転したか
    /// </summary>
    public override bool isLeftStickCounterClockwiseRotate
    {
        get
        {
            return _leftStickInput.isSuccessCounterClockwiseRotate;
        }
    }


    #endregion

    #region right

    /// <summary>
    /// 右スティックの向きを取得
    /// </summary>
    public override Vector3 getRightStickDirection
    {
        get
        {
            return _rightStickInput.getDirection;
        }
    }

    /// <summary>
    /// 右スティックの水平方向軸の取得
    /// </summary>
    public override float getRightHorizontal
    {
        get
        {
            return _rightStickInput.getHorizontal;
        }
    }

    /// <summary>
    /// 右スティックの垂直方向軸の取得
    /// </summary>
    public override float getRightVertical
    {
        get
        {
            return _rightStickInput.getVertical;
        }
    }

    /// <summary>
    /// 右スティックが右に傾いているか
    /// </summary>
    public override bool isRightStickRightDirection
    {
        get
        {
            return _rightStickInput.isDirectionRight;
        }
    }

    /// <summary>
    /// 右スティックが左に傾いているか
    /// </summary>
    public override bool isRightStickLeftDirection
    {
        get
        {
            return _rightStickInput.isDirectionLeft;
        }
    }

    /// <summary>
    /// 右スティックが億に傾いているか
    /// </summary>
    public override bool isRightStickFrontDirection
    {
        get
        {
            return _rightStickInput.isDirectionFront;
        }
    }

    /// <summary>
    /// 右スティックが手前に傾いているか
    /// </summary>
    public override bool isRightStickBackDirection
    {
        get
        {
            return _rightStickInput.isDirectionBack;
        }
    }

    /// <summary>
    /// 右スティックが左奥に傾いているか
    /// </summary>
    public override bool isRightStickUpperLeftDirection
    {
        get
        {
            return _rightStickInput.isDirectionUpperLeft;
        }
    }

    /// <summary>
    /// 右スティックが右奥に傾いているか
    /// </summary>
    public override bool isRightStickUpperRightDirection
    {
        get
        {
            return _rightStickInput.isDirectionUpperRight;
        }
    }

    /// <summary>
    /// 右スティックが左手前に傾いているか
    /// </summary>
    public override bool isRightStickLowerLeftDirection
    {
        get
        {
            return _rightStickInput.isDirectionLowerLeft;
        }
    }

    /// <summary>
    /// 右スティックが右手前に傾いているか
    /// </summary>
    public override bool isRightStickLowerRightDirection
    {
        get
        {
            return _rightStickInput.isDirectionLowerRight;
        }
    }

    /// <summary>
    /// 右スティックが左フリックしたか
    /// </summary>
    public override bool isRightStickLeftFlick
    {
        get
        {
            return _rightStickInput.isFlickLeft;
        }
    }

    /// <summary>
    /// 右スティックが左フリックしたか
    /// </summary>
    public override bool isRightStickRightFlick
    {
        get
        {
            return _rightStickInput.isFlickRight;
        }
    }

    /// <summary>
    /// 右スティックが奥にフリックしたか
    /// </summary>
    public override bool isRightStickFrontFlick
    {
        get
        {
            return _rightStickInput.isFlickFront;
        }
    }

    /// <summary>
    /// 右スティックが手前にフリックしたか
    /// </summary>
    public override bool isRightStickBackFlick
    {
        get
        {
            return _rightStickInput.isFlickBack;
        }
    }

    /// <summary>
    /// 右スティックが左奥にフリックしたか
    /// </summary>
    public override bool isRightStickUpperLeftFlick
    {
        get
        {
            return _rightStickInput.isFlickUpperLeft;
        }
    }

    /// <summary>
    /// 右スティックが右奥にフリックしたか
    /// </summary>
    public override bool isRightStickUpperRightFlick
    {
        get
        {
            return _rightStickInput.isFlickUpperRight;
        }
    }

    /// <summary>
    /// 右スティックが左手前にフリックしたか
    /// </summary>
    public override bool isRightStickLowerLeftFlick
    {
        get
        {
            return _rightStickInput.isFlickLowerLeft;
        }
    }

    /// <summary>
    /// 右スティックが右手前にフリックしたか
    /// </summary>
    public override bool isRightStickLowerRightFlick
    {
        get
        {
            return _rightStickInput.isFlickLowerRight;
        }
    }

    /// <summary>
    /// 右スティックがフリックしたか
    /// </summary>
    public override bool isRightStickFlick
    {
        get
        {
            return _rightStickInput.isFlick;
        }
    }

    /// <summary>
    /// 右スティックが右回転したか
    /// </summary>
    public override bool isRightStickClockwiseRotate
    {
        get
        {
            return _rightStickInput.isSuccessClockwiseRotate;
        }
    }

    /// <summary>
    /// 右スティックが左回転したか
    /// </summary>
    public override bool isRightStickCounterClockwiseRotate
    {
        get
        {
            return _rightStickInput.isSuccessCounterClockwiseRotate;
        }
    }

    /// <summary>
    /// インビジブルコマンドが入力されたか
    /// </summary>
    public override bool isCommandInvisible
    {
        get
        {
            return _rightStickInput.isCommandInvisible;
        }
    }

    #endregion

    #region button

    /// <summary>
    /// R2を押しているか
    /// </summary>
    public override bool isPressR2
    {
        get
        {
            return _gamePadManager.GetTriggerValue(_type) > 0.0f;
        }
    }

    /// <summary>
    /// R2を押したか
    /// </summary>
    public override bool isPushR2
    {
        get
        {
            if (_isR2Push) return false;
            return isPressR2;
        }
    }

    /// <summary>
    /// R2離したか
    /// </summary>
    public override bool isReleaseR2
    {
        get
        {
            if (!_isR2Push) return false;
            return _gamePadManager.GetTriggerValue(_type) == 0.0f;
        }
    }

    /// <summary>
    /// L2を押しているか
    /// </summary>
    public override bool isPressL2
    {
        get
        {
            return _gamePadManager.GetTriggerValue(_type) < 0.0f;
        }
    }

    /// <summary>
    /// L2を押したか
    /// </summary>
    public override bool isPushL2
    {
        get
        {
            if (_isL2Push) return false;
            return isPressL2;
        }
    }

    /// <summary>
    /// L2離したか
    /// </summary>
    public override bool isReleaseL2
    {
        get
        {
            if (!_isL2Push) return false;
            return _gamePadManager.GetTriggerValue(_type) == 0.0f;
        }
    }

    /// <summary>
    /// 攻撃ボタンを押したか
    /// </summary>
    public override bool isAttack
    {
        get
        {
            return isPushR2 || IsPush(GamePadManager.ButtonType.A);
        }
    }

    /// <summary>
    /// 移動していたか
    /// </summary>
    public override bool isMoved
    {
        get
        {
            return getLeftStickDirection != Vector3.zero;
        }
    }

    /// <summary>
    /// 左ステップしたか
    /// </summary>
    public override bool isRightEscape
    {
        get
        {
            return IsPush(GamePadManager.ButtonType.R1);
        }
    }

    /// <summary>
    /// 右ステップしたか
    /// </summary>
    public override bool isLeftEscape
    {
        get
        {
            return IsPush(GamePadManager.ButtonType.L1);
        }
    }

    /// <summary>
    /// スキルボタンを押したか
    /// </summary>
    public override bool isSkill
    {
        get
        {
            return IsPush(GamePadManager.ButtonType.R1);
        }
    }

    /// <summary>
    /// 防御ボタンを押したか
    /// </summary>
    public override bool isDefence
    {
        get
        {
            return IsPush(GamePadManager.ButtonType.B);
        }
    }

    #endregion

    //bool isCommandSpeedChangeSkill
    //{
    //    get
    //    {
    //        const float REACTION_VALUE = 0.5f;
    //        var right_x = getRightStickDirection.x;
    //        var left_x = getLeftStickDirection.x;

    //        var right_z = getRightStickDirection.z;
    //        var left_z = getLeftStickDirection.z;

    //        if (left_x == 0.0f && right_z == 0.0f) return false;

    //        if (left_x + REACTION_VALUE > right_x && left_x - REACTION_VALUE < right_x)
    //        {
    //            if (left_z + REACTION_VALUE > right_z && left_z - REACTION_VALUE < right_z)
    //            {
    //                return true;
    //            }
    //        } 
    //        return false;
    //    }
    //}

    //----------------------------------------------------------------

    GamePadManager _gamePadManager = null;
    LeftStickInput _leftStickInput = null;
    RightStickInput _rightStickInput = null;

    bool _isR2Push = false;
    bool _isL2Push = false;

    void Start()
    {
        _gamePadManager = GamePadManager.instance;
        _leftStickInput = new LeftStickInput(_gamePadManager, _type);
        _rightStickInput = new RightStickInput(_gamePadManager, _type);
    }

    void Update()
    {
    }

    void LateUpdate()
    {
        _leftStickInput.Update();
        _rightStickInput.Update();

        TriggerUpdate(isPressR2, isReleaseR2, ref _isR2Push);
        TriggerUpdate(isPressL2, isReleaseL2, ref _isL2Push);

    }

    /// <summary>
    /// 押したか離したかをチェックするブール値の更新
    /// </summary>
    /// <param name="is_push_trigger">押したか</param>
    /// <param name="is_release_trigger">離したか</param>
    /// <param name="is_push">ブール値を入れる</param>
    void TriggerUpdate(bool is_push_trigger, bool is_release_trigger, ref bool is_push)
    {
        if (is_push_trigger)
        {
            is_push = true;
        }
        
        if (is_release_trigger)
        {
            is_push = false;
        }
    }
}
