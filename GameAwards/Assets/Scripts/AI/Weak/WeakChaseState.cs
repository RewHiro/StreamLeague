/// <author>
/// 新井大一
/// </author>
/// 
using UnityEngine;

/// <summary>
/// プレイヤーを追いかける機能
/// </summary>
public class WeakChaseState : State<WeakAI>
{
    const float CHASE_TIME = 3.0f;

    public WeakChaseState(WeakAI weak_ai):
        base(weak_ai)
    {
        _attack = _stateManager.GetComponent<Attack>();
    }

    public override void Enter()
    {
        var players = GameObject.FindGameObjectsWithTag(TagName.Player);
        _targetTransform = players.ObjectCloseToTheSecond(_stateManager.transform.position);
        if (_targetTransform == null) _stateManager.ChangeState(_stateManager.getRandomState);
    }

    public override void Execute()
    {
        if (_targetTransform == null) _stateManager.ChangeState(_stateManager.getRandomState);

        var vector = _targetTransform.position - _stateManager.transform.position;
        _stateManager.aiInput.leftStickDirection = vector.normalized;

        if (_time >= CHASE_TIME) _stateManager.ChangeState(_stateManager.getRandomState);

        if (!_attack.isCanAttack) _stateManager.ChangeState(_stateManager.getRandomState);

        if (_attack.isInRange) _stateManager.ChangeState(WeakAI.State.ATTACK);

        _time += Time.deltaTime;
    }

    public override void Exit()
    {
        _time = 0.0f;
    }

    Attack _attack = null;
    Transform _targetTransform = null;

    float _time = 0.0f;
}