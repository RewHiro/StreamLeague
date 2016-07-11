/// <author>
/// 新井大一
/// </author>

/// <summary>
/// フェードの状態
/// </summary>
public class FadeState
{

    public FadeState()
    {
        state = State.WAIT;
    }

    /// <summary>
    /// 状態
    /// </summary>
    public enum State
    {
        //フェードイン
        IN,
        //フェードアウト
        OUT,
        //待機状態
        WAIT
    }

    /// <summary>
    /// 状態を取得、設定
    /// </summary>
    public State state
    {
        get; set;
    }

    /// <summary>
    /// フェード状態か
    /// </summary>
    public bool isFade
    {
        get
        {
            return state != State.WAIT;
        }
    }

    /// <summary>
    /// フェードイン状態か
    /// </summary>
    public bool isFadeIn
    {
        get
        {
            return state == State.IN;
        }
    }

    /// <summary>
    /// フェードアウト状態か
    /// </summary>
    public bool isFadeOut
    {
        get
        {
            return state == State.OUT;
        }
    }
}