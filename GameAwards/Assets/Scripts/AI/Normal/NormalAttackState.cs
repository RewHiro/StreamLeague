/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 攻撃する機能
/// </summary>
public class NormalAttackState : State<NormalAI>
{
    public NormalAttackState(NormalAI normal_ai)
        : base(normal_ai)
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
        if (_attack.isCanAttack) _stateManager.ChangeState(NormalAI.State.COLLECT);
        _stateManager.ChangeState(NormalAI.State.COLLECT);
    }

    public override void Exit()
    {
        _stateManager.aiInput.attack = false;
    }

    Attack _attack = null;
}
