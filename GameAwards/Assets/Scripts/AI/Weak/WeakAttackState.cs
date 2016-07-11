/// <author>
/// 新井大一
/// </author>

using UnityEngine;

/// <summary>
/// 攻撃する機能
/// </summary>
public class WeakAttackState : State<WeakAI>
{
    public WeakAttackState(WeakAI weak_ai):
        base(weak_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
    }

    public override void Enter()
    {
        _stateManager.isActive = false;
        _stateManager.aiInput.leftStickDirection = Vector3.zero;
        _stateManager.aiInput.attack = true;
    }

    public override void Execute()
    {
        if (_attack.isCanAttack) _stateManager.ChangeState(WeakAI.State.COLLECT);
        _stateManager.ChangeState(WeakAI.State.COLLECT);
    }

    public override void Exit()
    {
        _stateManager.aiInput.attack = false;
    }

    Attack _attack = null;
}