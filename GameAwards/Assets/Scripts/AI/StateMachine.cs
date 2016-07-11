/// <author>
/// 新井大一
/// </author>

/// <summary>
/// ステートマシン
/// </summary>
public class StateMachine<T>
{
    public StateMachine()
    {
        currentState = null;
    }

    /// <summary>
    /// 現在の状態を取得
    /// </summary>
    public State<T> currentState
    {
        get; private set;
    }

    /// <summary>
    /// ステートを変更
    /// </summary>
    /// <param name="state">EnumState</param>
    public void ChangeState(State<T> state)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState == null) return;
        currentState.Execute();
    }
}