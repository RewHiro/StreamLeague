/// <author>
/// 新井大一
/// </author>

public class NormalStepState : State<NormalAI>
{
    public NormalStepState(NormalAI normal_ai) :
        base(normal_ai)
    {

    }

    public override void Enter()
    {
        _stateManager.aiInput.leftEscape = true;
        _stateManager.ChangeState(NormalAI.State.COLLECT);
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        _stateManager.aiInput.leftEscape = false;
    }
}
