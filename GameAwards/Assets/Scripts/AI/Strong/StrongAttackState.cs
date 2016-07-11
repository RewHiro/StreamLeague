/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 攻撃する機能
/// </summary>
public class StrongAttackState : State<StrongAI>
{

    public StrongAttackState(StrongAI strong_ai)
        : base(strong_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
    }

    public override void Enter()
    {
        _stateManager.aiInput.leftStickDirection = Vector3.zero;
        _stateManager.aiInput.attack = true;
    }

    public override void Execute()
    {
        if (_attack.isCanAttack) _stateManager.ChangeState(StrongAI.State.COLLECT);
        _stateManager.ChangeState(_stateManager.getRandomState);
    }

    public override void Exit()
    {
        _stateManager.aiInput.attack = false;
    }

    Attack _attack = null;
}
