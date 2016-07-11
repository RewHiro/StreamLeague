/// <author>
/// 新井大一
/// </author>

using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ゲームパッドのアナログスティック入力の抽象クラス
/// </summary>
abstract class StickInput
{
    /// <summary>
    /// 反応するフリックの移動量
    /// </summary>
    const float REACTION_FLICK_VALUE = 0.3f;
    const float INPUT_INTERVAL_TIME = 0.5f;

    // 回転が成功した回数のマックス値
    const int SUCCESSED_COUNT = 3;

    public StickInput
        (
            GamePadManager gamePadManager,
            GamePadManager.Type type
        )
    {
        _gamePadManager = gamePadManager;
        _type = type;

        _rotateStateActions.Add(RotateState.WAIT, WaitStateUpdate);
        _rotateStateActions.Add(RotateState.RIGHT, RightOrLeftStateUpdate);
        _rotateStateActions.Add(RotateState.BACK, FrontOrBackStateUpdate);
        _rotateStateActions.Add(RotateState.FRONT, FrontOrBackStateUpdate);
        _rotateStateActions.Add(RotateState.LEFT, RightOrLeftStateUpdate);
        _rotateStateActions.Add(RotateState.SUCCESS, ResetRotateData);

        _horizontalStateActions.Add(HorizontalState.WAIT, HorizontalWaitUpdate);
        _horizontalStateActions.Add(HorizontalState.RIGHT, HorizontalRightUpdate);
        _horizontalStateActions.Add(HorizontalState.LEFT, HorizontalLeftUpdate);
        _horizontalStateActions.Add(HorizontalState.SUCCESS, HorizontalSuccessUpdate);
    }

    /// <summary>
    /// 水平方軸の取得
    /// </summary>
    public abstract float getHorizontal { get; }

    /// <summary>
    /// 垂直方向軸の取得
    /// </summary>
    public abstract float getVertical { get; }

    /// <summary>
    /// 向きを取得
    /// </summary>
    public Vector3 getDirection
    {
        get
        {
            return getVector.normalized;
        }
    }

    /// <summary>
    /// 左に傾いているか
    /// </summary>
    public bool isDirectionLeft
    {
        get
        {
            return getDirection.x <= -0.9f;
        }
    }

    /// <summary>
    /// 右に傾いているか
    /// </summary>
    public bool isDirectionRight
    {
        get
        {
            return getDirection.x >= 0.9f;
        }
    }

    /// <summary>
    /// 奥に傾いているか
    /// </summary>
    public bool isDirectionFront
    {
        get
        {
            return getDirection.z >= 0.9f;
        }
    }

    /// <summary>
    /// 手前に傾いているか
    /// </summary>
    public bool isDirectionBack
    {
        get
        {
            return getDirection.z <= -0.9f;
        }
    }

    /// <summary>
    /// 左スティックが右奥に傾いているか
    /// </summary>
    public bool isDirectionUpperRight
    {
        get
        {
            var is_horizontal = 0.6f <= getDirection.x && 0.8f <= getDirection.x;
            var is_vertical = 0.6f <= getDirection.z && 0.8f <= getDirection.z;
            return is_horizontal && is_vertical;
        }
    }

    /// <summary>
    /// 左スティックが左奥に傾いているか
    /// </summary>
    public bool isDirectionUpperLeft
    {
        get
        {
            var is_horizontal = -0.8f <= getDirection.x && -0.6f <= getDirection.x;
            var is_vertical = 0.6f <= getDirection.z && 0.8f <= getDirection.z;
            return is_horizontal && is_vertical;
        }
    }

    /// <summary>
    /// 左スティックが右手前に傾いているか
    /// </summary>
    public bool isDirectionLowerRight
    {
        get
        {
            var is_horizontal = 0.6f <= getDirection.x && 0.8f <= getDirection.x;
            var is_vertical = -0.8f <= getDirection.z && -0.6f <= getDirection.z;
            return is_horizontal && is_vertical;
        }
    }

    /// <summary>
    /// 左スティックが左手前に傾いたか
    /// </summary>
    public bool isDirectionLowerLeft
    {
        get
        {
            var is_horizontal = -0.8f <= getDirection.x && -0.6f <= getDirection.x;
            var is_vertical = -0.8f <= getDirection.z && -0.6f <= getDirection.z;
            return is_horizontal && is_vertical;
        }
    }

    /// <summary>
    /// 左に傾いたか
    /// </summary>
    public bool isDownLeft
    {
        get
        {
            if (_isDownLeft) return false;
            return isDirectionLeft;
        }
    }

    /// <summary>
    /// 右に傾いたか
    /// </summary>
    public bool isDownRight
    {
        get
        {
            if (_isDownRight) return false;
            return isDirectionRight;
        }
    }

    /// <summary>
    /// 奥に傾いたか
    /// </summary>
    public bool isDownFront
    {
        get
        {
            if (_isDownFront) return false;
            return isDirectionFront;
        }
    }

    /// <summary>
    /// 手前に傾いたか
    /// </summary>
    public bool isDownBack
    {
        get
        {
            if (_isDownBack) return false;
            return isDirectionBack;
        }
    }

    /// <summary>
    /// 左スティックが左奥に傾いたか
    /// </summary>
    public bool isDownUpperLeft
    {
        get
        {
            if (_isDownUpperLeft) return false;
            return isDirectionUpperLeft;
        }
    }

    /// <summary>
    /// 左スティックが右奥に傾いたか
    /// </summary
    public bool isDownUpperRight
    {
        get
        {
            if (_isDownUpperRight) return false;
            return isDirectionUpperRight;
        }
    }

    /// <summary>
    /// 左スティックが左手前に傾いたか
    /// </summary>
    public bool isDownLowerLeft
    {
        get
        {
            if (_isDownLowerLeft) return false;
            return _isDownLowerLeft;
        }
    }

    /// <summary>
    /// 左スティックが右手前に傾いたか
    /// </summary>
    public bool isDownLowerRight
    {
        get
        {
            if (_isDownLowerRight) return false;
            return _isDownLowerRight;
        }
    }

    /// <summary>
    /// フリックしたか
    /// </summary>
    public bool isFlick
    {
        get
        {
            if (_isFlicked) return false;
            if (!isMoreScalar(0.8f)) return false;
            var distacne = Vector3.Distance(_prevVector, getVector);
            return distacne > REACTION_FLICK_VALUE;
        }
    }

    /// <summary>
    /// 左にフリックしたか
    /// </summary>
    public bool isFlickLeft
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionLeft) return false;
            return true;
        }
    }

    /// <summary>
    /// 右にフリックしたか
    /// </summary>
    public bool isFlickRight
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionRight) return false;
            return true;
        }
    }

    /// <summary>
    /// 奥にフリックしたか
    /// </summary>
    public bool isFlickFront
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionFront) return false;
            return true;
        }
    }

    /// <summary>
    /// 手前にフリックしたか
    /// </summary>
    public bool isFlickBack
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionBack) return false;
            return true;
        }
    }

    /// <summary>
    /// 左スティックが左奥にフリックしたか
    /// </summary>
    public bool isFlickUpperLeft
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionUpperLeft) return false;
            return true;
        }
    }

    /// <summary>
    /// 左スティックが右奥にフリックしたか
    /// </summary>
    public bool isFlickUpperRight
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionUpperRight) return false;
            return true;
        }
    }

    /// <summary>
    /// 左スティックが左手前にフリックしたか
    /// </summary>
    public bool isFlickLowerLeft
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionLowerLeft) return false;
            return true;
        }
    }

    /// <summary>
    /// 左スティックが右手前にフリックしたか
    /// </summary>
    public bool isFlickLowerRight
    {
        get
        {
            if (!isFlick) return false;
            if (!isDirectionLowerRight) return false;
            return true;
        }
    }


    /// <summary>
    /// 右回転に成功したか
    /// </summary>
    public bool isSuccessClockwiseRotate
    {
        get
        {
            if (!_isClockwise) return false;
            return _rotateState == RotateState.SUCCESS;
        }
    }

    /// <summary>
    /// 左回転に成功したか
    /// </summary>
    public bool isSuccessCounterClockwiseRotate
    {
        get
        {
            if (_isClockwise) return false;
            return _rotateState == RotateState.SUCCESS;
        }
    }

    /// <summary>
    /// インビジブルコマンドが成功したか
    /// </summary>
    public bool isCommandInvisible
    {
        get
        {
            return _horizontalState == HorizontalState.SUCCESS;
        }
    }

    public void Update()
    {
        StickUpdate(isDirectionLeft, ref _isDownLeft);
        StickUpdate(isDirectionRight, ref _isDownRight);
        StickUpdate(isDirectionFront, ref _isDownFront);
        StickUpdate(isDirectionBack, ref _isDownBack);
        StickUpdate(isDirectionLowerLeft, ref _isDownLowerLeft);
        StickUpdate(isDirectionLowerRight, ref _isDownLowerRight);
        StickUpdate(isDirectionUpperLeft, ref _isDownUpperLeft);
        StickUpdate(isDirectionUpperRight, ref _isDownUpperRight);

        FlickUpdate();
        RotateStateUpdate();
        HorizontalStateUpdate();

        _prevVector = getVector;
    }

    //-----------------------------------------------

    protected GamePadManager _gamePadManager;
    protected GamePadManager.Type _type;

    /// <summary>
    /// ベクトルを取得
    /// </summary>
    protected Vector3 getVector
    {
        get
        {
            return new Vector3(getHorizontal, 0, getVertical);
        }
    }

    /// <summary>
    /// ベクトルの大きさが1以上か
    /// </summary>
    protected bool isOneMoreScalar
    {
        get
        {
            var vector = getVector;
            var scalr = Vector3.Dot(vector, vector);
            return scalr >= 1.0f;
        }
    }

    /// <summary>
    /// 水平方向軸の大きさが1以上か
    /// </summary>
    protected bool isHorizontalOneMoreScalar
    {
        get
        {
            var horizontal = getHorizontal;
            var scalr = horizontal * horizontal;
            return scalr >= 1.0f;
        }
    }

    /// <summary>
    /// 垂直方向軸の大きさが1以上か
    /// </summary>
    protected bool isVerticalOneMoreScalar
    {
        get
        {
            var vertical = getVertical;
            var scalr = vertical * vertical;
            return scalr >= 1.0f;
        }
    }

    /// <summary>
    /// ベクトルの大きさが1以上か
    /// </summary>
    protected bool isMoreScalar(float value)
    {
        var vector = getVector;
        var scalr = Vector3.Dot(vector, vector);
        return scalr >= value;
    }

    //------------------------------------------------------------

    // スティックの回転状態
    enum RotateState
    {
        RIGHT,
        LEFT,
        FRONT,
        BACK,
        WAIT,
        SUCCESS
    }

    // 水平方向の状態
    enum HorizontalState
    {
        RIGHT,
        LEFT,
        WAIT,
        SUCCESS
    }

    //　1フレーム前のベクトル情報
    Vector3 _prevVector = Vector3.zero;

    // フリックに必要な情報
    bool _isFlicked = false;

    //　倒したかどうかの判定に必要な情報
    bool _isDownLeft = false;
    bool _isDownRight = false;
    bool _isDownFront = false;
    bool _isDownBack = false;
    bool _isDownUpperLeft = false;
    bool _isDownUpperRight = false;
    bool _isDownLowerLeft = false;
    bool _isDownLowerRight = false;


    //　回転しているかの判定に必要な情報
    Dictionary<RotateState, Action> _rotateStateActions = new Dictionary<RotateState, Action>();
    RotateState _rotateState = RotateState.WAIT;
    float _inputIntervalRotateCount = 0.0f;
    int _successRotateCount = 0;
    bool _isClockwise = false;

    // 水平方向軸のレバガチャの判定に必要な情報
    Dictionary<HorizontalState, Action> _horizontalStateActions = new Dictionary<HorizontalState, Action>();
    HorizontalState _horizontalState = HorizontalState.WAIT;
    float _inputIntervalHorizontalCount = 0.0f;
    int _successHorizontalCount = 0;

    /// <summary>
    /// 回転情報をリセットするか
    /// </summary>
    bool isReset
    {
        get
        {
            return _inputIntervalRotateCount > INPUT_INTERVAL_TIME || RotateState.SUCCESS == _rotateState || getVector == Vector3.zero;
        }
    }

    /// <summary>
    /// フリックできるかチェック
    /// </summary>
    bool checkCanFlick
    {
        get
        {
            var vector = getVector;
            var scalr = Vector3.Dot(vector, vector);
            return scalr >= 0.8f;
        }
    }

    /// <summary>
    /// 傾きの更新
    /// </summary>
    /// <param name="direction">向き</param>
    /// <param name="is_down">倒したかの真理値</param>
    void StickUpdate(bool direction, ref bool is_down)
    {
        if (direction)
        {
            is_down = true;
        }
        else
        {
            is_down = false;
        }
    }

    /// <summary>
    /// フリックの更新
    /// </summary>
    void FlickUpdate()
    {
        if (!checkCanFlick)
        {
            _isFlicked = false;
        }

        if (isFlick || checkCanFlick)
        {
            _isFlicked = true;
        }
    }

    /// <summary>
    /// 回転ステートの更新
    /// </summary>
    void RotateStateUpdate()
    {
        IntervalCountUpdate();
        CheckReset();

        _rotateStateActions[_rotateState]();
    }

    /// <summary>
    /// 回転ステートの待機状態の更新
    /// </summary>
    void WaitStateUpdate()
    {
        if (!isDirectionRight) return;
        _rotateState = RotateState.RIGHT;
        _successRotateCount++;
    }

    /// <summary>
    /// 回転ステートの右か左の状態の更新
    /// </summary>
    void RightOrLeftStateUpdate()
    {
        if (isDirectionFront)
        {
            SuccessOrNextUpdate(false, RotateState.FRONT);
        }
        else if (isDirectionBack)
        {
            SuccessOrNextUpdate(true, RotateState.BACK);
        }
    }

    /// <summary>
    /// 回転ステートの成功したか次の状態にいくかの更新
    /// </summary>
    /// <param name="count">成功した回数</param>
    /// <param name="is_clockwise">時計周りか</param>
    /// <param name="rotate_state">次の状態にしたいステートを入れる</param>
    void SuccessOrNextUpdate(bool is_clockwise, RotateState rotate_state)
    {

        if (_successRotateCount == SUCCESSED_COUNT)
        {
            _isClockwise = is_clockwise;
            _rotateState = RotateState.SUCCESS;
        }
        else
        {

            _successRotateCount++;
            _rotateState = rotate_state;
        }
    }

    /// <summary>
    /// 回転ステートの奥か手前の更新
    /// </summary>
    void FrontOrBackStateUpdate()
    {
        if (!isDirectionLeft) return;
        _rotateState = RotateState.LEFT;
        _successRotateCount++;
    }

    /// <summary>
    /// 回転情報のデータのリセット
    /// </summary>
    void ResetRotateData()
    {
        _rotateState = RotateState.WAIT;
        _inputIntervalRotateCount = 0.0f;
        _successRotateCount = 0;
        _isClockwise = false;
    }

    /// <summary>
    /// 右入力が始まったらIntervalCountを増加していく
    /// </summary>
    void IntervalCountUpdate()
    {
        if (_rotateState == RotateState.WAIT) return;
        _inputIntervalRotateCount += Time.deltaTime;
    }

    /// <summary>
    /// リセットしてよかったらリセットする
    /// </summary>
    void CheckReset()
    {
        if (!isReset) return;
        ResetRotateData();
    }

    /// <summary>
    /// 水平方向ステートの待機状態の更新
    /// </summary>
    void HorizontalWaitUpdate()
    {
        if (isDirectionRight)
        {
            _successHorizontalCount++;
            _horizontalState = HorizontalState.RIGHT;
        }
        else if (isDirectionLeft)
        {
            _successHorizontalCount++;
            _horizontalState = HorizontalState.LEFT;
        }
    }

    /// <summary>
    /// 水平方向ステートの右状態の更新
    /// </summary>
    void HorizontalRightUpdate()
    {
        if (!isDirectionLeft) return;
        _successHorizontalCount++;
        _horizontalState = HorizontalState.LEFT;
        HorizontalSuccessUpdate();
    }

    /// <summary>
    /// 水平方向ステートの左状態の更新
    /// </summary>
    void HorizontalLeftUpdate()
    {
        if (!isDirectionRight) return;
        _successHorizontalCount++;
        _horizontalState = HorizontalState.RIGHT;
        HorizontalSuccessUpdate();
    }

    /// <summary>
    /// 水平方向のレバガチャで必要な情報をリセット
    /// </summary>
    void ResetHorizontalData()
    {
        _inputIntervalHorizontalCount = 0.0f;
        _successHorizontalCount = 0;
        _horizontalState = HorizontalState.WAIT;
    }

    /// <summary>
    /// レバガチャ成功時の更新
    /// </summary>
    void HorizontalSuccessUpdate()
    {
        if (_successHorizontalCount < SUCCESSED_COUNT) return;
        _horizontalState = HorizontalState.SUCCESS;
    }

    /// <summary>
    /// 水平方向のステート更新
    /// </summary>
    void HorizontalStateUpdate()
    {
        IntervalHorizontalCountUpdate();
        ChackResetHorizontalData();
        _horizontalStateActions[_horizontalState]();
    }

    /// <summary>
    /// 水平方向軸のカウント更新
    /// </summary>
    void IntervalHorizontalCountUpdate()
    {
        if (_horizontalState == HorizontalState.WAIT) return;
        _inputIntervalHorizontalCount += Time.deltaTime;
    }

    /// <summary>
    /// 水平方向の必要な情報をリセットするか
    /// </summary>
    void ChackResetHorizontalData()
    {
        if (_inputIntervalHorizontalCount < INPUT_INTERVAL_TIME) return;
        ResetHorizontalData();
    }
}