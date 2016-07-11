/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// AIが使用する仮想ゲームパッドの機能
/// </summary>
public class AIInput : InputBase
{
    /// <summary>
    /// 左スティックの向きの値
    /// </summary>
    public Vector3 leftStickDirection
    {
        get
        {
            return getLeftStickDirection;
        }
        set
        {
            getLeftStickDirection = value;
        }
    }

    /// <summary>
    /// 右スティックの向きの値
    /// </summary>
    public Vector3 rightStickDirection
    {
        get
        {
            return getRightStickDirection;
        }
        set
        {
            getRightStickDirection = value;
        }
    }

    /// <summary>
    /// R3を押したかの値
    /// </summary>
    public bool pushR3
    {
        get
        {
            return isPushR3;
        }

        set
        {
            isPushR3 = value;
        }
    }

    /// <summary>
    /// R3を離したかの値
    /// </summary>
    public bool releaseR3
    {
        get
        {
            return isReleaseR3;
        }

        set
        {
            isReleaseR3 = value;
        }
    }

    /// <summary>
    /// 左スティックを奥にフリックしたかの値
    /// </summary>
    public bool rightStickFrontFlick
    {
        get
        {
            return isRightStickFrontFlick;
        }

        set
        {
            isRightStickFrontFlick = value;
        }
    }

    /// <summary>
    /// 左スティックを手前にフリックしたかの値
    /// </summary>
    public bool rightStickBackFlick
    {
        get
        {
            return isRightStickBackFlick;
        }

        set
        {
            isRightStickBackFlick = value;
        }
    }

    /// <summary>
    /// 左スティックを右にフリックしたかの値
    /// </summary>
    public bool rightStickRightFlick
    {
        get
        {
            return isRightStickRightFlick;
        }

        set
        {
            isRightStickRightFlick = value;
        }
    }

    /// <summary>
    /// 左スティックを左にフリックしたかの値
    /// </summary>
    public bool rightStickLeftFlick
    {
        get
        {
            return isRightStickLeftFlick;
        }

        set
        {
            isRightStickLeftFlick = value;
        }
    }

    /// <summary>
    /// 左スティックをフリックしたかの値
    /// </summary>
    public bool rightStickFlick
    {
        get
        {
            return isRightStickFlick;
        }

        set
        {
            isRightStickFlick = value;
        }
    }

    /// <summary>
    /// インビジブルスキルボタン
    /// </summary>
    public bool commandInvisible
    {
        get
        {
            return isCommandInvisible;
        }

        set
        {
            isCommandInvisible = value;
        }
    }

    /// <summary>
    /// 左スティックを時計周りにしたかの値
    /// </summary>
    public bool rightStickClockwiseRotate
    {
        get
        {
            return isRightStickClockwiseRotate;
        }

        set
        {
            isRightStickClockwiseRotate = value;
        }
    }

    /// <summary>
    /// 左スティックを逆時計周りにしたかの値
    /// </summary>
    public bool counterClockwiseRotate
    {
        get
        {
            return isRightStickCounterClockwiseRotate;
        }

        set
        {
            isRightStickCounterClockwiseRotate = value;
        }
    }

    /// <summary>
    ///　攻撃ボタン
    /// </summary>
    public bool attack
    {
        get
        {
            return isAttack;
        }

        set
        {
            isAttack = value;
        }
    }

    /// <summary>
    /// 左ステップボタン
    /// </summary>
    public bool rightEscape
    {
        get
        {
            return isRightEscape;
        }

        set
        {
            isRightEscape = value;
        }
    }

    /// <summary>
    /// 右ステップボタン
    /// </summary>
    public bool leftEscape
    {
        get
        {
            return isLeftEscape;
        }

        set
        {
            isLeftEscape = value;
        }
    }

    /// <summary>
    /// スキルボタン
    /// </summary>
    public bool skill
    {
        get
        {
            return isSkill;
        }

        set
        {
            isSkill = value;
        }
    }

    /// <summary>
    /// 防御ボタン
    /// </summary>
    public bool defence
    {
        get
        {
            return isDefence;
        }

        set
        {
            isDefence = value;
        }
    }

}