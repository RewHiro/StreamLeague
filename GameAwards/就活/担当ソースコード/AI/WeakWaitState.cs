using UnityEngine;

/// <summary>
/// 待機する機能
/// </summary>
public class WeakWaitState : State<WeakAI>
{
    float _waitTime = 1.0f;

    public WeakWaitState(WeakAI weak_ai):
        base(weak_ai)
    {

    }

    public override void Enter()
    {
        _stateManager.aiInput.leftStickDirection = Vector3.zero;
        _waitTime = UnityEngine.Random.Range(3.0f, 5.0f);
    }

    public override void Execute()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime) return;
        _stateManager.ChangeState(_stateManager.getRandomState);
    }

    public override void Exit()
    {
        _time = 0.0f;
    }

    float _time = 0.0f;
}
