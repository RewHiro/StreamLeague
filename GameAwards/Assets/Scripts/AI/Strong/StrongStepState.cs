/// <author>
/// 新井大一
/// </author>
/// 
public class StrongStepState : State<StrongAI>
{

    public StrongStepState(StrongAI strong_ai) :
        base(strong_ai)
    {

    }

    public override void Enter()
    {
        _stateManager.aiInput.leftEscape = true;
        _stateManager.ChangeState(StrongAI.State.COLLECT);
    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        _stateManager.aiInput.leftEscape = false;
    }
}
