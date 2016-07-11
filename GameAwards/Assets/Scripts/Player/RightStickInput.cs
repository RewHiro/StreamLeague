/// <author>
/// 新井大一
/// </author>


/// <summary>
/// 右スティックの管理クラス
/// </summary>
class RightStickInput : StickInput
{
    public RightStickInput
        (
            GamePadManager gamePadManager,
            GamePadManager.Type type
        ) :
        base(gamePadManager, type)
    {

    }

    /// <summary>
    /// 水平方向軸の取得
    /// </summary>
    public override float getHorizontal
    {
        get
        {
            return _gamePadManager.GetRightHorizontal(_type);
        }
    }

    /// <summary>
    /// 垂直方向軸の取得
    /// </summary>
    public override float getVertical
    {
        get
        {
            return _gamePadManager.GetRightVertical(_type);
        }
    }
}
